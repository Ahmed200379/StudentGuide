using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;
using StudentGuide.API.Helpers;
using StudentGuide.BLL.Dtos.Account;
using StudentGuide.BLL.Services.Email;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace StudentGuide.BLL.Services.AccountService
{
    public class AccountService : IAccountService
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailingService _mailingService;
        private readonly IHelper _helper;
        private readonly IMemoryCache _memoryCache;
        public AccountService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IHelper helper,IMailingService mailingService, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _helper = helper;
            _mailingService=mailingService;
            _memoryCache=memoryCache;
        }


        public async Task<MessageResponseDto> ForgetPassword(string email)
        {
            var user=await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = "There is no user with this email"
                };
            }
            var code = new Random().Next(100000, 999999).ToString();
            
            var resetToken =await _userManager.GeneratePasswordResetTokenAsync(user);
            _memoryCache.Set($"Reset_{email}", new { Token = resetToken, Code = code }, TimeSpan.FromMinutes(15));
            await _mailingService.SendEmailAsync(user.Email, "Password Reset Code",
                                              $"Your password reset code is: {code}");
            return new MessageResponseDto
            {
                Date = DateTime.Now,
                IsSuccessed = true,
                Message = "Send Message Successfully"
            };
        }

        public async Task<ResonseDto> Login(LoginDto loginDto)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
            ResonseDto resonseDto = new ResonseDto();
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                resonseDto.Message = "Email or password is incorrect";
                return resonseDto;
            }
           var token= await _helper.CreateToken(user);
            resonseDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            resonseDto.Message = "You Successfully Login";
            resonseDto.ExpiresIn = token.ValidTo;
            resonseDto.StudentId = user.Id;
            resonseDto.IsAuthenticated = true;
            resonseDto.Role = Enum.TryParse<Role>(await _userManager.GetRolesAsync(user).ContinueWith(r => r.Result.FirstOrDefault()), out var role) ? role : Role.Student;
            return resonseDto;
        }

        public async Task<ResonseDto> Register(RegisterDto registerDto)
        {
            var isRegisterEmail = await _userManager.FindByEmailAsync(registerDto.StudentEmail);
            var isRegisterCode = await _userManager.FindByIdAsync(registerDto.StudentId);

            if (isRegisterEmail != null || isRegisterCode != null)
            {
                return new ResonseDto
                {
                    Message = "This account is already registered"
                };
            }
                if(!registerDto.StudentEmail.Contains("fci"))
                {
                    throw new Exception("InValid College Email");
                }
            Student newStudent = new()
            {
                Name = registerDto.StudentName,
                Code = registerDto.StudentId,
                Email = registerDto.StudentEmail,
                Password = registerDto.StudentPassword,
                Photo = registerDto.StudentPhoto != null
                ? await _helper.SaveImage(registerDto.StudentPhoto)
                : null,
                BirthDate = registerDto.BirthDateOfStudent,
                PhoneNumber = registerDto.PhoneNumber,
                Semester = "Semester1",
                DepartmentCode = "GN",
                Gpa = 0,
                Hours = 0
            };
            await _unitOfWork.StudentRepo.AddAsync(newStudent);
            var isAdded = await _unitOfWork.Complete();
            if (isAdded == 0)
            {
                throw new Exception(Constant.Exceptions.ExceptionMessages.GetAddFailedMessage("student"));
            }
            ApplicationUser applicationUser = new()
                {
                    Student = newStudent,
                    UserName = registerDto.StudentName.Replace(" ", ""),
                    Email = registerDto.StudentEmail,
                    Id = registerDto.StudentId,
                };
                var result =await _userManager.CreateAsync(applicationUser,registerDto.StudentPassword);
                if (!result.Succeeded)
                {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                await _unitOfWork.StudentRepo.Delete(newStudent);
                await _unitOfWork.Complete();
                throw new Exception($"Registration failed: {errors}");
               
                }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.Student.ToString()));
            }
            await _userManager.AddToRoleAsync(applicationUser,Role.Student.ToString());
                
                var jwtToken = await _helper.CreateToken(applicationUser);
                return new ResonseDto
                {
                    Role = Role.Student,
                    Message = "Registeration Successed",
                    IsAuthenticated = true,
                    ExpiresIn = jwtToken.ValidTo,
                    StudentId = registerDto.StudentId,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
                };
        }

        public async Task<MessageResponseDto> ResetPassword(ResetPasswordDto newPass)
        {
            var user =await _userManager.FindByEmailAsync(newPass.Email);
            if (user == null)
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = "There is no user in Specific email"
                };
            }
            if (!_memoryCache.TryGetValue($"Reset_{newPass.Email}", out dynamic cacheData))
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = "Reset code expired or invalid"
                };
            }
            var result = await _userManager.ResetPasswordAsync(user, cacheData.Token, newPass.Password);
            if(!result.Succeeded)
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = result.Errors.ToString()
                };
            }
            return new MessageResponseDto
            {
                Date = DateTime.Now,
                IsSuccessed = true,
                Message = "You successfully Reset Password"
            };

        }

        public async Task<MessageResponseDto> ValidateCode(CheckCodeDto code)
        {

            var user = await _userManager.FindByEmailAsync(code.Email);
            if (user == null)
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = "There is no user in Specific email"
                };
            }
            if (!_memoryCache.TryGetValue($"Reset_{code.Email}", out dynamic cacheData))
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = "Reset code expired or invalid"
                };
            }
            if (cacheData.Code != code.Code)
            {
                return new MessageResponseDto
                {
                    Date = DateTime.Now,
                    Message = "Reset code is incorrect"
                };
            }
            return new MessageResponseDto
            {
                Date = DateTime.Now,
                IsSuccessed = true,
                Message = "Code is valid, you can reset your password"
            };
        }
    }
}

using Microsoft.AspNetCore.Identity;
using StudentGuide.API.Helpers;
using StudentGuide.BLL.Dtos.Account;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;

namespace StudentGuide.BLL.Services.AccountService
{
    public class AccountService : IAccountService
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHelper _helper;
        public AccountService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _helper = helper;
        }

        public async Task<RegisterationResonseDto> Register(RegisterDto registerDto)
        {
            var isRegisterEmail = await _userManager.FindByEmailAsync(registerDto.StudentEmail);
            var isRegisterName = await _userManager.FindByNameAsync(registerDto.StudentName);
            var isRegisterCode = await _userManager.FindByIdAsync(registerDto.StudentId);

            if (isRegisterEmail != null || isRegisterName != null || isRegisterCode != null)
            {
                return new RegisterationResonseDto
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
                    Semester = registerDto.Semester,
                    DepartmentCode = registerDto.DepartmentCode,
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
                    UserName = registerDto.StudentName,
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
                return new RegisterationResonseDto
                {
                    Role = Role.Student,
                    Message = "Registeration Successed",
                    IsAuthenticated = true,
                    ExpiresIn = jwtToken.ValidTo,
                    StudentId = registerDto.StudentId,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
                };
        }

    }
}

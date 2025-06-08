using Microsoft.AspNetCore.Identity;
using StudentGuide.BLL.Dtos.Account;
using StudentGuide.BLL.Dtos.Admin;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.ManagementService
{
    public class ManagementService : IManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManagementService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        public async Task<MessageResponseDto> AddAdmin(AdminAddDto adminAddDto)
        {
            var isRegisterEmail = await _userManager.FindByEmailAsync(adminAddDto.Email);
            var isRegisterName = await _userManager.FindByNameAsync(adminAddDto.Name);
            var isRegisterCode = await _userManager.FindByIdAsync(adminAddDto.Code);

            if (isRegisterEmail != null || isRegisterName != null || isRegisterCode != null)
            {
                return new MessageResponseDto
                {
                    Message = "This account is already registered"
                };
            }
            if (!adminAddDto.Email.Contains("fci"))
            {
                throw new Exception("InValid College Email");
            }
          
            ApplicationUser applicationUser = new()
            {
                UserName = adminAddDto.Name,
                Email = adminAddDto.Email,
                Id = adminAddDto.Code,
            };
            var result = await _userManager.CreateAsync(applicationUser, adminAddDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");

            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            }
            await _userManager.AddToRoleAsync(applicationUser, Role.Admin.ToString());
            return new MessageResponseDto
            {
                Message = "The Admin Added Successfully",
                Date = DateTime.Now,
                IsSuccessed = true
            };
        }

      
    }
}

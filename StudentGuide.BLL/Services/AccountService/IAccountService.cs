using StudentGuide.BLL.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.AccountService
{
   public interface IAccountService
    {
        public Task<ResonseDto> Register(RegisterDto registerDto);
        public Task<ResonseDto> Login(LoginDto loginDto);
        public Task<MessageResponseDto> ForgetPassword(string email);
        public Task<bool> ValidateCode(CheckCodeDto code);
        public Task<MessageResponseDto> ResetPassword(ResetPasswordDto newPass);
    }
}

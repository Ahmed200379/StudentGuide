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
        public Task<RegisterationResonseDto> Register(RegisterDto registerDto);
    }
}

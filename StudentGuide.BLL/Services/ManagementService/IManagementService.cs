using StudentGuide.BLL.Dtos.Account;
using StudentGuide.BLL.Dtos.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.ManagementService
{
   public interface IManagementService
    {
        public Task<MessageResponseDto> AddAdmin(AdminAddDto adminAddDto);
    }
}

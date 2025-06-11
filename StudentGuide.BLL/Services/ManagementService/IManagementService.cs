using StudentGuide.BLL.Dtos.Account;
using StudentGuide.BLL.Dtos.Admin;
using StudentGuide.BLL.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.ManagementService
{
   public interface IManagementService
    {
        public Task<MessageResponseDto> SendDepartmentNotificationEmail(EmailRequestDto emailRequestDto);
        public Task<MessageResponseDto> AddAdmin(AdminAddDto adminAddDto);
        public Task<MessageResponseDto> SendMessageToEmail(EmailRequestDto emailRequestDto);
    }
}

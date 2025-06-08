using StudentGuide.BLL.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Email
{
   public interface IMailingService
    {
        Task<MessageResponseDto> SendEmailAsync(string email, string subject, string body);

    }
}

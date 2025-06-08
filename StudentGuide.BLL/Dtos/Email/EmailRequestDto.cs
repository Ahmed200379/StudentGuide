using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Email
{
   public class EmailRequestDto
    {
        public string Subject { get; set; }=string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Account
{
   public class MessageResponseDto
    {
        public bool IsSuccessed { get; set; }
        public String Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}

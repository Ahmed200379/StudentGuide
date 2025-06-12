using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Result
{
   public class ResultDeleteDto
    {
        public string studentId { get; set; } = string.Empty;
        public string courseCode { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Result
{
   public class ResultAddDto
    {
        public String StudentId { get; set; } = String.Empty;
        public string CourseId { get; set; } = String.Empty;
        public int GradeOfFinal { get; set; }
        public int GradeWithoutFinal { get; set; }
    }
}

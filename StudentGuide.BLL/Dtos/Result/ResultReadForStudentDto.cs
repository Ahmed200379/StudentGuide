using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Result
{
  public class ResultReadForStudentDto
    {
        public String StudentId { get; set; } = String.Empty;
        public String StudentName { get; set;} = String.Empty;
        public string CourseName { get; set;} = String.Empty;
        public String Grade { get; set;} = String.Empty;
    }
}

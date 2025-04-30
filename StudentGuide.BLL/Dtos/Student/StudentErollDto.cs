using StudentGuide.BLL.Dtos.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Student
{
   public class StudentErollDto
    {
        public string StudentId { get; set; } = string.Empty;
        public int MaxHours { get; set; }
        public List<String> Codes { get; set; } = new List<string>();
    }
}

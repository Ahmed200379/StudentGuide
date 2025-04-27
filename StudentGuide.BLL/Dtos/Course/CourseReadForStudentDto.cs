using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Course
{
   public class CourseReadForStudentDto
    {
        public String Code { get; set; }=string.Empty;
        public String NameOfCourse { get; set; } = string.Empty;
        public int Hourse {  get; set; }

    }
}

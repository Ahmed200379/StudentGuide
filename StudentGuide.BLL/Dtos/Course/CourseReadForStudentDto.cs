using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Course
{
   public class CourseReadForStudentDto
    {
        public IEnumerable<CourseReadDto> AllAvaliableCourses { get; set; } = new List<CourseReadDto>();
        public int HoursOfStudent { get; set; }
        public int MaxHours { get; set; }
    }
}

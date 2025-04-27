using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Course
{
  public  class CourseReadDto
    {
        public String Code { get; set; } = string.Empty;
        public String NameOfCourse { get; set; } = string.Empty;
        public List<String> PreRequestCoursesCode { get; set; } = new List<string>();
        public int HoursOfCourse { get; set; }
        public List<String> Semesters { get; set; } = new List<String>();
        public List<String> DepartmentIds { get; set; } = new List<String>();
        public bool MandatoryCourse { get; set; }
    }
}

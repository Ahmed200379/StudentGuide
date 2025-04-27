using StudentGuide.BLL.Dtos.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Course
{
  public  class CourseReadPagnationDto
    {
        public IEnumerable<CourseReadDto> Courses { get; set; } = new List<CourseReadDto>();
        public int TotalCount { get; set; }
    }
}

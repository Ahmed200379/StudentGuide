using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Student
{
  public class StudentReadWithCountDto
    {
        public IEnumerable<StudentReadForAdminDto> StudentReadForAdmins { get; set; }=new List<StudentReadForAdminDto>();
        public int TotalCount { get; set; }
    }
}

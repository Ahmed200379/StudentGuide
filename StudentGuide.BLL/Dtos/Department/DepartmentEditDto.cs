using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Department
{
    public class DepartmentEditDto
    {
        public String DepartmentCode { get; set; } = string.Empty;
        public String NameOfDepartment { get; set; } = string.Empty;
    }
}

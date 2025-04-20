using StudentGuide.BLL.Dtos.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Departments
{
   public interface IDepartmentService
    {
        public Task<bool> AddDepartment(DepartmentAddDto department);
    }
}

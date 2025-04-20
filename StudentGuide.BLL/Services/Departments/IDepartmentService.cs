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
        public Task<bool> UpdateDepartment(DepartmentEditDto department);
        public Task<bool> DeleteDepartment(string id);
        public Task<List<DepartmentReadDto>> GetAllDepartment();   
        public Task<DepartmentReadDto?> GetDepartmentById(string id);
    }
}

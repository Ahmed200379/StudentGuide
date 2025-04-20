using StudentGuide.BLL.Dtos.Department;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Departments
{
   public class DepartmentService:IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddDepartment(DepartmentAddDto department)
        {
            if (department == null)
                return false;
            else
            {
                Department newDepartment = new()
                {
                    Code=department.DepartmentCode,
                    Name=department.NameOfDepartment
                };
                 await _unitOfWork.DepartmentRepo.AddAsync(newDepartment);
                int isSaved = await _unitOfWork.Complete();
                return isSaved > 0;
            }
        }
    }
}

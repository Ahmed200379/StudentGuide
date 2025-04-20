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

        public async Task<bool> UpdateDepartment(DepartmentEditDto department)
        {
            if (department == null)
                return false;

                Department? oldDepartment= await _unitOfWork.DepartmentRepo.GetByIdAsync(department.DepartmentCode);

                if (oldDepartment == null)
                    return false;

                oldDepartment.Name=department.NameOfDepartment;
                await _unitOfWork.DepartmentRepo.Update(oldDepartment);
                int isUpdated= await _unitOfWork.Complete();
                return isUpdated > 0;

        }

        public async Task<bool> DeleteDepartment(string id)
        {
            Department? deletedDepartment = await _unitOfWork.DepartmentRepo.GetByIdAsync(id);
            if (deletedDepartment is null)
                return false;

            await _unitOfWork.DepartmentRepo.Delete(deletedDepartment);
            int issaved = await _unitOfWork.Complete();
            return issaved > 0;
        }

        public async Task<List<DepartmentReadDto>> GetAllDepartment()
        {
            var departments = await _unitOfWork.DepartmentRepo.GetAll();
              var departmentReadDtos = departments
                .Select(d=>new DepartmentReadDto
                {
                    DepartmentCode=d.Code,
                    NameOfDepartment=d.Name
                }).ToList();
            return departmentReadDtos;
        }

        public async Task<DepartmentReadDto?> GetDepartmentById(string id)
        {
            Department? department=await _unitOfWork.DepartmentRepo.GetByIdAsync(id);
            if (department is null) return null;
            var departmentReadDto = new DepartmentReadDto
            {
                DepartmentCode = department.Code,
                NameOfDepartment = department.Name
            };
            return departmentReadDto;

        }
    }
}

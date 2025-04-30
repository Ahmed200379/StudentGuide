using StudentGuide.BLL.Dtos.Material;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Students
{
  public interface IStudentService
    {
        public Task<int> GetMaxHours(string code);
        public Task AddNewStudent(StudentAddDto newStudent);
        public Task EditStudent(StudentEditDto editStudent);
        public Task<StudentReadWithCountDto> GetAllSudentsWithCount();
        public Task<StudentReadForAdminDto> GetByIdForAdmin(string code);
        public Task<StudentReadForStudentDto> GetByIdForStudent(string code);
        public Task EnrollCourses(StudentErollDto studentErollDto);
        public Task Delete(string code);
        public Task<StudentReadWithCountDto> GetAllStudentsInPagnation(int page, int countPerPage);
    }
}

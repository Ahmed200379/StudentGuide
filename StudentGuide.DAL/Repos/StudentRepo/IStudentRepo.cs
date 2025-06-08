using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.StudentRepo
{
   public interface IStudentRepo:IBaseRepo<Student>
    {
        public Task<IEnumerable<Student>> GetAllStudentsInPagnation(int count, int countPerPage);
        public Task<IEnumerable<Student>> GetAllAsync(Expression<Func<Student, bool>>? expression = null);
        public Task AddRangeAsync(List<StudentCourse> studentCourses);
        public Task<Student?> GetByIdAsync(String code);
        public Task<IEnumerable<Student>> GetAll();


    }
}

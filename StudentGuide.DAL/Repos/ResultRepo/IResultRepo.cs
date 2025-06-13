using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.ResultRepo
{
   public interface IResultRepo:IBaseRepo<StudentCourse>
    {
        public void UpdateRangeAsync(List<StudentCourse> results);
        public StudentCourse GetById(string studentId, string courseCode);
        public Task<IEnumerable<StudentCourse>> GetAllWithIncludeAsync(Expression<Func<StudentCourse, bool>>? expression = null);
        public Task<int> GetHoursOfCourse(string courseCode);
        public Task<StudentCourse> GetByStudentAndCourseAsync(string studentId, string courseCode);

    }
}

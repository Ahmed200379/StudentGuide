using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System.Linq.Expressions;
using System.Linq;

namespace StudentGuide.DAL.Repos.ResultRepo
{
  public class ResultRepo: BaseRepo<StudentCourse>,IResultRepo
    {
        private readonly ApplicationDbContext _context;
        public ResultRepo(ApplicationDbContext context):base(context) { _context = context; }

        public StudentCourse GetById(string studentId, string courseCode)
        {
            return _context.Set<StudentCourse>()
                .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseCode == courseCode)!;
        }

        public void UpdateRangeAsync(List<StudentCourse> results)
        {
             _context.Set<StudentCourse>().UpdateRange(results);
        }
        public async Task<IEnumerable<StudentCourse>> GetAllWithIncludeAsync(Expression<Func<StudentCourse, bool>>? expression = null)
        {
            IQueryable<StudentCourse> allData = _context.Set<StudentCourse>();

            if (expression != null)
                allData = allData.Where(expression);

            allData = allData.Include(s => s.Course).Include(s => s.Student);

            return await allData.AsNoTracking().ToListAsync();
        }

        public Task<int> GetHoursOfCourse(string courseCode)
        {
            return _context.Set<StudentCourse>()
                .Where(sc => sc.CourseCode == courseCode).Include(courseCode => courseCode.Course)
                .Select(sc => sc.Course.Hours)
                .FirstOrDefaultAsync();
        }
    }
}

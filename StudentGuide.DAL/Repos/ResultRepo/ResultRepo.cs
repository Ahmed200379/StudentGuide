using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;

namespace StudentGuide.DAL.Repos.ResultRepo
{
  public class ResultRepo: BaseRepo<StudentCourse>,IResultRepo
    {
        private readonly ApplicationDbContext _context;
        public ResultRepo(ApplicationDbContext context):base(context) { _context = context; }
        public void UpdateRangeAsync(List<StudentCourse> results)
        {
             _context.Set<StudentCourse>().UpdateRange(results);
        }
    }
}

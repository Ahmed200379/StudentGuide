using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.CourseRepo
{
    public class CourseRepo : BaseRepo<Course>, ICourseRepo
    {
      private readonly ApplicationDbContext _context;
      public CourseRepo(ApplicationDbContext dbContext): base(dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesInPagnation(int page, int countPerPage)
        {
            if (page < 1) page = 1;
            if (countPerPage < 1) countPerPage = 10;

            var courses = await _context.Courses
            .OrderBy(c => c.Name)
            .Skip((page - 1) * countPerPage)
            .Take(countPerPage).ToListAsync();
            return courses;
        }
    }
}

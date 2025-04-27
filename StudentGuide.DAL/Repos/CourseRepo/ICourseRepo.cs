using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.CourseRepo
{
   public interface ICourseRepo :IBaseRepo<Course>
    {
        public Task<IEnumerable<Course>> GetAllCoursesInPagnation(int page, int countPerPage);
    }
}

using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;

namespace StudentGuide.DAL.Repos.CourseRepo
{
   public interface ICourseRepo :IBaseRepo<Course>
    {
        public Task<IEnumerable<Course>> GetAllCoursesInPagnation(int page, int countPerPage);
    }
}

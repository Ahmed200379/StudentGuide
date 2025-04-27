using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Courses
{
   public interface ICourseService
    {
        public Task AddCourse(CourseAddDto newCourse);
        public Task<IEnumerable<CourseReadDto>> GetAllCourses();
        public Task EditCourse(CourseEditDto course);
        public Task<CourseReadDto?> GetCourseById(string id);
        public Task DeleteCourse(int id);
        public Task<CourseReadPagnationDto> GetAllCoursesInPagnation(int page, int countPerPage);
        public Task<List<CourseReadDto>> Search(String Keyword);
    }
}

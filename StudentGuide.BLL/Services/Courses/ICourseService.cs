using StudentGuide.BLL.Dtos.Course;
namespace StudentGuide.BLL.Services.Courses
{
   public interface ICourseService
    {
        public Task AddCourse(CourseAddDto newCourse);
        public Task<IEnumerable<CourseReadDto>> GetAllCourses();
        public Task EditCourse(CourseEditDto course);
        public Task<CourseReadDto?> GetCourseById(string id);
        public Task DeleteCourse(string code);
        public Task<CourseReadPagnationDto> GetAllCoursesInPagnation(int page, int countPerPage);
        public Task<List<CourseReadDto>> Search(String Keyword);
        public Task<CourseReadForStudentDto> GetAllCoursesForStudent(string code);
        public Task<IEnumerable<CourseReadDto>> GetRecommendationCourses(string code);
    }
}

using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.Data.Models;

namespace StudentGuide.API.Helpers
{
    public interface IHelper
    {
        CourseReadDto MapToCourseReadDto(Course course);
        public void MapStudentEditDtoToStudent(StudentEditDto editStudent, Student student);
        public bool HasDuplicates(List<string> items);
        public int GoToNextSemester(int hours, int currentSemester);
        public double CalculatePointForCourse(int grade);
        public double CalculateGPA(IEnumerable<StudentCourse> passedCourses);
        public String GetGradeWithSymbol(int grade);
    }
}

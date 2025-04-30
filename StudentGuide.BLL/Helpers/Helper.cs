using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.Data.Models;

namespace StudentGuide.API.Helpers
{
    public class Helper : IHelper
    {
        public bool HasDuplicates(List<string> items)
        {
            return items.Count != items.Distinct().Count();
        }
        public CourseReadDto MapToCourseReadDto(Course course)
        {
            return new CourseReadDto
            {
                Code = course.Code,
                NameOfCourse = course.Name,
                HoursOfCourse = course.Hours,
                MandatoryCourse = course.IsCompulsory,
                PreRequestCoursesCode = course.PrerequisiteCourses?.ToList() ?? new List<string>(),
                Semesters = course.Semesters.ToList(),
                DepartmentIds = course.CourseDepartments.Select(d => d.DepartmentsCode).ToList()
            };
        }
        public void MapStudentEditDtoToStudent(StudentEditDto editStudent, Student student)
        {
            student.Code = editStudent.StudentId;
            student.Name = editStudent.StudentName;
            student.Email = editStudent.StudentEmail;
            student.Password = editStudent.StudentPassword;
            student.Gpa = editStudent.StudentGpa;
            student.Hours = editStudent.TotalHours;
            student.Date = editStudent.DateOfRegister;
            student.Photo = editStudent.StudentPhoto;
            student.BirthDate = editStudent.BirthDateOfStudent;
            student.PhoneNumber = editStudent.PhoneNumber;
            student.Semester = editStudent.Semester;
            student.DepartmentCode = editStudent.DepartmentCode;
        }

    }

}

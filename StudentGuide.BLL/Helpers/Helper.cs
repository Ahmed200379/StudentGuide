using StudentGuide.BLL.Constant;
using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;

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
            student.Photo = editStudent.StudentPhoto;
            student.BirthDate = editStudent.BirthDateOfStudent;
            student.PhoneNumber = editStudent.PhoneNumber;
            student.Semester = editStudent.Semester;
            student.DepartmentCode = editStudent.DepartmentCode;
        }
        public int GoToNextSemester(int hours, int currentSemester)
        {
            if((currentSemester == 2 && hours >= 28)|| (currentSemester == 4 && hours >= 62)|| (currentSemester == 6 && hours >= 98))
            {
                    return ++currentSemester;
            }
            else
            {
                return currentSemester;
            }
        }

        public double CalculatePointForCourse(int grade)
        {
            if (grade >= 90) return 4.0;
            else if (grade >= 85) return 3.7;
            else if (grade >= 80) return 3.3;
            else if (grade >= 75) return 3.0;
            else if (grade >= 70) return 2.7;
            else if (grade >= 65) return 2.4;
            else if (grade >= 60) return 2.2;
            else if (grade >= 50) return 2.0;
            else return 0.0;
        }
        public double CalculateGPA(IEnumerable<StudentCourse> passedCourses)
        {
            double totalPoints = 0;
            double totalHours = 0;

            foreach (var course in passedCourses)
            {
                double points = CalculatePointForCourse(course.Grade);
                totalPoints += points * course.Course.Hours;
                totalHours += course.Course.Hours;
            }

            if (totalHours == 0) return 0;

            return totalPoints / totalHours;
        }
        public string GetGradeWithSymbol(int grade)
        {
            if (grade >= 90) return "A+";
            else if (grade >= 85) return "A";
            else if (grade >= 80) return "B+";
            else if (grade >= 75) return "B";
            else if (grade >= 70) return "C+";
            else if (grade >= 65) return "C";
            else if (grade >= 60) return "D+";
            else if (grade >= 50) return "D";
            else return "F";
        }
    }

}

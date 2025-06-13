using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentGuide.BLL.Constant;
using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentGuide.API.Helpers
{
    public class Helper : IHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public Helper(UserManager<ApplicationUser> userManager,IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;   
        }

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
        public async void MapStudentEditDtoToStudent(StudentEditDto editStudent, Student student)
        {
            student.Code = editStudent.StudentId;
            student.Name = editStudent.StudentName;
            student.Email = editStudent.StudentEmail;
            student.Password = editStudent.StudentPassword;
            student.Gpa = 0;
            student.Hours = 0;
            student.Photo = await SaveImage(editStudent.StudentPhoto);
            student.BirthDate = editStudent.BirthDateOfStudent;
            student.PhoneNumber = editStudent.PhoneNumber;
            student.Semester = "Semester1";
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
                return --currentSemester;
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
        public async Task<double> CalculateGPA(IEnumerable<StudentCourse> passedCourses)
        {
            double totalPoints = 0;
            double totalHours = 0;

            foreach (var course in passedCourses)
            {
                double points = CalculatePointForCourse(course.Grade);
                totalPoints += points * await _unitOfWork.ResultRepo.GetHoursOfCourse(course.CourseCode);
                totalHours += await _unitOfWork.ResultRepo.GetHoursOfCourse(course.CourseCode);
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

        public async Task<string> SaveImage(IFormFile file)
        {
            var photo = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(photo,ConstantData.ImagesPath);
            using var steam =File.Create(path);
            await file.CopyToAsync(steam);
            return photo;
        }

        public async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            //key from appsetting
            string? secretKey = _configuration.GetValue<string>("SecretKey");
            //encoding
            var key= Encoding.UTF8.GetBytes(secretKey);

            SymmetricSecurityKey semetricKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(semetricKey,SecurityAlgorithms.HmacSha256);

            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim(ClaimTypes.Name,user.UserName));
            userClaims.Add(new Claim (ClaimTypes.Email,user.Email));
            var userRoles= await _userManager.GetRolesAsync(user);
            foreach(var role in  userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role,role));
            }

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://studentguideapi.runasp.net/",
                audience:"http://localhost:4200/",
                expires: DateTime.Now.AddHours(1),
                claims:userClaims,
                signingCredentials:signingCredentials
                );
            return token;
        }
    }

}

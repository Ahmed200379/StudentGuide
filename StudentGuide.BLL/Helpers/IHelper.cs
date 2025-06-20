﻿using Microsoft.AspNetCore.Http;
using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.Data.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StudentGuide.API.Helpers
{
    public interface IHelper
    {
        CourseReadDto MapToCourseReadDto(Course course);
        public void MapStudentEditDtoToStudent(StudentEditDto editStudent, Student student);
        public bool HasDuplicates(List<string> items);
        public int GoToNextSemester(int hours, int currentSemester);
        public double CalculatePointForCourse(int grade);
        public Task<double> CalculateGPA(IEnumerable<StudentCourse> passedCourses);
        public String GetGradeWithSymbol(int grade);
        public Task<String> SaveImage(IFormFile file);
        public Task<JwtSecurityToken> CreateToken(ApplicationUser user);
    }
}

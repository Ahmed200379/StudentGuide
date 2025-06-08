using StudentGuide.DAL.Data.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace StudentGuide.BLL.Dtos.Account
{
   public class RegisterDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentPassword { get; set; } = string.Empty;
        [Compare("StudentPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public double StudentGpa { get; set; } = 0;
        public int TotalHours { get; set; } = 0;
        public IFormFile? StudentPhoto { get; set; } = default!;
        public DateTime BirthDateOfStudent { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Semester { get; set; } = "Semester1";
        public Role? role { get; set; } = Role.Student;
        public string DepartmentCode { get; set; } = string.Empty;
    }
}

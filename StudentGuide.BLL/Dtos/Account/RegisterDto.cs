using StudentGuide.DAL.Data.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace StudentGuide.BLL.Dtos.Account
{
   public class RegisterDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        [EmailAddress]
        [MaxLength(50), MinLength(10)]
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentPassword { get; set; } = string.Empty;
        [Compare("StudentPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public IFormFile? StudentPhoto { get; set; } = default!;
        public DateTime BirthDateOfStudent { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

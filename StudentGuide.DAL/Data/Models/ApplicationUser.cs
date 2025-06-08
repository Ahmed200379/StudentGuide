using Microsoft.AspNetCore.Identity;
namespace StudentGuide.DAL.Data.Models
{
   public class ApplicationUser:IdentityUser
    {
        public string? StudentCode { get; set; }
        public virtual Student? Student { get; set; }
        public Role role { get; set; }
    }
    public enum Role
    {
        Admin,
        Student,
    }
}

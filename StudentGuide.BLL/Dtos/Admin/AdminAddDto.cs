using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Admin
{
   public class AdminAddDto
    {
        [Required]
        public string Code { get; set; }=string.Empty;
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        public String Password { get; set; }=string.Empty;
        [Compare("Password",ErrorMessage = "Passwords do not match")]
        public String ConfirmPassword { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public String Email { get; set; }=string.Empty;
    }
}

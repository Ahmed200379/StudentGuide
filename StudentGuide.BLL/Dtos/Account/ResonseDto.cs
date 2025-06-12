using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Account
{
   public class ResonseDto
    {
        public string StudentId { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; } 
        public string Token { get; set; }
    }
}

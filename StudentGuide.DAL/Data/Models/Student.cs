using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
    public class Stduent : Base
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [MaxLength(50), MinLength(10)]
        public String Email { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
        [StringLength(5)]
        public Double Gpa { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public String Photo { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        [MaxLength(11)]
        public String PhoneNumber { get; set; } = string.Empty;
        public int Semester { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Department Department { get; set; }
        [ForeignKey("Department")]
        public string DepartmentCode { get; set; }

    }
}

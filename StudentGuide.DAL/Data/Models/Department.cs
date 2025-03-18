using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
    public class Department
    {
        [Key]
        public String Code { get; set; } = string.Empty;
        public virtual ICollection<Stduent> Stduents { get; set; } = new List<Stduent>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}

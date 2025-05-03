using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
    public class Department:Base
    {
        [Key]
        public String Code { get; set; } = string.Empty;
        public virtual ICollection<Student> Stduents { get; set; } = new List<Student>();
        public virtual ICollection<CourseDepartment> CourseDepartments { get; set; } = new List<CourseDepartment>();

    }
}

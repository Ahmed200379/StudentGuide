using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
    public class Course : Base
    {
        [Key]
        public String Code { get; set; } = string.Empty;
        public List<String> Prerequisite_Course { get; set; }
        [MaxLength(20), MinLength(2)]
        public String Group { get; set; } = string.Empty;
        [Range(2, 4)]
        public int Hours { get; set; }
        public bool IsCompulsory { get; set; } = false;
        public List<int> Semesters { get; set; }
        public virtual ICollection<Stduent> Stduents { get; set; } = new List<Stduent>();
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    }
}

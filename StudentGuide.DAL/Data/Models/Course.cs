using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
namespace StudentGuide.DAL.Data.Models
{
    public class Course : Base
    {
        [Key]
        public string Code { get; set; } = string.Empty;

        [Range(1, 4)]
        public int Hours { get; set; }
        public bool IsCompulsory { get; set; } = false;

        public List<String> Semesters { get; set; }= new List<string>() {"Semester1"};
        public List<String>? PrerequisiteCourses { get; set; }
        public virtual ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
        public virtual ICollection<CourseDepartment> CourseDepartments { get; set; } = new List<CourseDepartment>();
    }
}


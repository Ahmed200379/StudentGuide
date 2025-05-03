using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
        public class StudentCourse
        {
            [ForeignKey(nameof(Student))]
            public String StudentId { get; set; }
            public Student Student { get; set; }

            [ForeignKey(nameof(Course))]
            public string CourseCode { get; set; }
            public Course Course { get; set; }

            public bool IsPassed { get; set; } = false;

            [Range(-1, 101)]
            public int Grade { get; set; }
            public String Semester { get; set; }=string.Empty;
        }
}

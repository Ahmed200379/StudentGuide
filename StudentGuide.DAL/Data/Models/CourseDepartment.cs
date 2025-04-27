using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
  public  class CourseDepartment
    {
        [ForeignKey("Course")]
        public string CoursesCode { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Department")]
        public string DepartmentsCode { get; set; }
        public Department Department { get; set; }
    }
}

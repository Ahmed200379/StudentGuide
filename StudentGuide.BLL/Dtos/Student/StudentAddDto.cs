using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Student
{
   public class StudentAddDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentPassword { get; set; } = string.Empty;
        public double StudentGpa { get; set; }
        public int TotalHours { get; set; }
        public string StudentPhoto { get; set; } = string.Empty;
        public DateTime BirthDateOfStudent { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Semester { get; set; } = "Semester1";
        public string DepartmentCode {  get; set; } = string.Empty;
    }
}

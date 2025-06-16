using GameZone.Validation;
using Microsoft.AspNetCore.Http;
using StudentGuide.BLL.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Student
{
  public class StudentEditDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty; 
        public string StudentPassword { get; set; } = string.Empty;
        public DateTime BirthDateOfStudent { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
    }
}

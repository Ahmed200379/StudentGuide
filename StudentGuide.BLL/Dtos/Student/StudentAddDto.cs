using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GameZone.Validation;
using StudentGuide.BLL.Constant;

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
        [AllowedExtention(ConstantData.AllowExtentions),
            MaxSize(ConstantData.maxSizeByByets)]
        public IFormFile StudentPhoto { get; set; } = default!;
        public DateTime BirthDateOfStudent { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Semester { get; set; } = "Semester1";
        public string DepartmentCode {  get; set; } = string.Empty;
    }
}

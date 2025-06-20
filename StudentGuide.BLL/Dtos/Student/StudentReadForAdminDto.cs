﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Student
{
   public class StudentReadForAdminDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentPassword { get; set; } = string.Empty;
        public double StudentGpa { get; set; }
        public int TotalHours { get; set; }
        public DateTime DateOfRegister { get; set; } = DateTime.Now;
        public DateTime BirthDateOfStudent { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Semester { get; set; }=string.Empty;
        public string StudentPhoto { get; set; } = string.Empty;
    }
}

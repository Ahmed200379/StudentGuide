namespace StudentGuide.BLL.Dtos.Student
{
   public class StudentReadForStudentDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public double StudentGpa { get; set; }
        public int TotalHours { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string StudentPhoto { get; set; } = string.Empty;
    }
}

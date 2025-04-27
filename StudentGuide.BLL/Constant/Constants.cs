using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Constant
{
    public static class Constants
    {
        public static readonly List<string> Semesters = new List<string>
        {
            "Summer", "Semester1", "Semester2", "Semester3", "Semester4",
            "Semester5", "Semester6", "Semester7", "Semester8"
        };

        public static readonly List<string> CourseTypes = new List<string>
        {
            "Compulsory", "Optional"
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Constant
{
    public static class ConstantData
    {

        public static readonly Dictionary<int, string> Grades = new Dictionary<int, string>
        {
            { 90, "A+" },
            { 80, "A" },
            { 70, "B" },
            { 60, "C" },
            { 50, "D" },
            { 0, "F" }
        };
        public static readonly Dictionary<string, int> SemestersByName = new Dictionary<string, int>
    {
        { "Summer", 0 },
        { "Semester1", 1 },
        { "Semester2", 2 },
        { "Semester3", 3 },
        { "Semester4", 4 },
        { "Semester5", 5 },
        { "Semester6", 6 },
        { "Semester7", 7 },
        { "Semester8", 8 }
    };
        public static readonly Dictionary<int, string> SemestersByNumber = new Dictionary<int, string>
    {
         { 0, "Summer" },
    { 1, "Semester1" },
    { 2, "Semester2" },
    { 3, "Semester3" },
    { 4, "Semester4" },
    { 5, "Semester5" },
    { 6, "Semester6" },
    { 7, "Semester7" },
    { 8, "Semester8" }
    };
        public const int MaxHoursPerSemester = 18;

        public const int PassingGrade = 50;
    }
}

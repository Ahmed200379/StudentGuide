using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.StudentRepo
{
   public class StudentRepo:BaseRepo<Student>,IStudentRepo
    {
        private readonly ApplicationDbContext _context;
        public StudentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<StudentCourse> studentCourses)
        {
            await _context.AddRangeAsync(studentCourses);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInPagnation(int page, int countPerPage)
        {
            if (page < 1) page = 1;
            if (countPerPage < 1) countPerPage = 10;

            var students = await _context.Stduents
            .OrderBy(s => s.Name)
            .Skip((page - 1) * countPerPage)
            .Take(countPerPage).ToListAsync();
            return students;

        }
    }
}

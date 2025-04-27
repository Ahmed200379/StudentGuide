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
        public StudentRepo(ApplicationDbContext context) : base(context) { }
    }
}

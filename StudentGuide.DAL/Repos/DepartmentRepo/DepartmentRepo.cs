using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.DepartmentRepo
{
    public class DepartmentRepo : BaseRepo<Department>, IDepartmentRepo
    {
       private readonly ApplicationDbContext _context;
        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
            _context= context;
        }
        
    }
}

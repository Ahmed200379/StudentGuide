using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos;
using StudentGuide.DAL.Repos.BaseRepo;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.UnitOfWork
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;


        Repos.MaterialRepo.IMaterialRepo _MaterialRepo { get; }
        IDepartmentRepo _departmentRepo { get; }

        Repos.MaterialRepo.IMaterialRepo IUnitOfWork.MaterialRepo => _MaterialRepo;

        public IDepartmentRepo DepartmentRepo => _departmentRepo;

        public UnitOfWork(ApplicationDbContext context, Repos.MaterialRepo.IMaterialRepo materialRepo, IDepartmentRepo departmentRepo)
        {
            _context = context;
            _MaterialRepo = materialRepo;
            _departmentRepo= departmentRepo;
        }

        async Task<int> IUnitOfWork.Complete()
        {
            return await _context.SaveChangesAsync();
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}

using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Repos.MaterialRepo.IMaterialRepo MaterialRepo { get; }
        public Task<int> Complete();
    }
}

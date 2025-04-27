using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos;
using StudentGuide.DAL.Repos.BaseRepo;
using StudentGuide.DAL.Repos.CourseRepo;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMaterialRepo MaterialRepo { get; }
        IDepartmentRepo DepartmentRepo { get; }
        ICourseRepo CourseRepo { get; }
        public Task<int> Complete();
    }
}

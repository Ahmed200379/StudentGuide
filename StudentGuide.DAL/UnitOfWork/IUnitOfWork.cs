using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos;
using StudentGuide.DAL.Repos.BaseRepo;
using StudentGuide.DAL.Repos.CourseRepo;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.DocumentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.Repos.PaymentRepo;
using StudentGuide.DAL.Repos.ResultRepo;
using StudentGuide.DAL.Repos.StudentRepo;
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
        IStudentRepo StudentRepo { get; }
        IResultRepo ResultRepo { get; }
        IDocumentRepo DocumentRepo { get; }
        IPaymentRepo PaymentRepo { get; }
        public Task<int> Complete();
    }
}

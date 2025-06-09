using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Repos.CourseRepo;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.DocumentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.Repos.ResultRepo;
using StudentGuide.DAL.Repos.StudentRepo;

namespace StudentGuide.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IMaterialRepo _materialRepo;
        private readonly IDepartmentRepo _departmentRepo;
        private readonly ICourseRepo _courseRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IResultRepo _resultRepo;
        private readonly IDocumentRepo _documentRepo;   
        public IMaterialRepo MaterialRepo => _materialRepo;
        public IDepartmentRepo DepartmentRepo => _departmentRepo;
        public ICourseRepo CourseRepo => _courseRepo;
        public IStudentRepo StudentRepo => _studentRepo;

        public IResultRepo ResultRepo => _resultRepo;
        public IDocumentRepo DocumentRepo => _documentRepo;

        public UnitOfWork(ApplicationDbContext context, IMaterialRepo materialRepo, IDepartmentRepo departmentRepo, ICourseRepo courseRepo,IStudentRepo studentRepo,IResultRepo resultRepo,IDocumentRepo documentRepo)
        {
            _context = context;
            _materialRepo = materialRepo;
            _departmentRepo = departmentRepo;
            _courseRepo = courseRepo;
            _studentRepo= studentRepo;
            _resultRepo = resultRepo;
            _documentRepo = documentRepo;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

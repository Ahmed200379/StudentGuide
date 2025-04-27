using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Repos.CourseRepo;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
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
        public IMaterialRepo MaterialRepo => _materialRepo;
        public IDepartmentRepo DepartmentRepo => _departmentRepo;
        public ICourseRepo CourseRepo => _courseRepo;
        public IStudentRepo StudentRepo => _studentRepo;
        public UnitOfWork(ApplicationDbContext context, IMaterialRepo materialRepo, IDepartmentRepo departmentRepo, ICourseRepo courseRepo,IStudentRepo studentRepo)
        {
            _context = context;
            _materialRepo = materialRepo;
            _departmentRepo = departmentRepo;
            _courseRepo = courseRepo;
            _studentRepo= studentRepo;
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

using StudentGuide.BLL.Services.HashId;
using StudentGuide.BLL.Services.Materials;
using StudentGuide.BLL.Services.Departments;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.Repos.StudentRepo;
using StudentGuide.DAL.UnitOfWork;
using System.Collections;
using StudentGuide.DAL.Repos.CourseRepo;
using Microsoft.Extensions.DependencyInjection;
using StudentGuide.BLL.Services.Courses;
using StudentGuide.BLL.Services.Students;
using StudentGuide.API.Helpers;
using StudentGuide.BLL.Services.Results;
using StudentGuide.DAL.Repos.ResultRepo;
using StudentGuide.BLL.Services.AccountService;

namespace StudentGuide.API
{
    public static class ApplicationServices
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IMaterialRepo, MaterialRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentRepo,StudentRepo>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IResultRepo, ResultRepo>();
            services.AddScoped<IResultsService, ResultsService>();
            services.AddScoped<IAccountService,AccountService>();
            services.AddScoped<IHelper, Helper>();
            services.AddScoped<HashIdService>();

        }
    }
}

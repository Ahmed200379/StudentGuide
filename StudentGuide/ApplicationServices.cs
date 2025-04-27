using StudentGuide.BLL.Services.HashId;
using StudentGuide.BLL.Services.Materials;
using StudentGuide.BLL.Services.Departments;
using StudentGuide.DAL.Repos.DepartmentRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.UnitOfWork;
using System.Collections;
using StudentGuide.DAL.Repos.CourseRepo;
using Microsoft.Extensions.DependencyInjection;
using StudentGuide.BLL.Services.Courses;

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
            services.AddScoped<HashIdService>();

        }
    }
}

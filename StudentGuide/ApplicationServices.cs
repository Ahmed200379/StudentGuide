using StudentGuide.BLL.Services.Materials;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.UnitOfWork;
using System.Collections;

namespace StudentGuide.API
{
    public static class ApplicationServices
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IMaterialRepo, MaterialRepo>();

        }
    }
}

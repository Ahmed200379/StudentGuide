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
using StudentGuide.BLL.Services.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using StudentGuide.BLL.Services.ManagementService;
using StudentGuide.BLL.Services.DocumentService;
using StudentGuide.DAL.Repos.DocumentRepo;
using StudentGuide.DAL.Repos.PaymentRepo;
using StudentGuide.BLL.Services.PaymentService;

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
            services.AddScoped<IMailingService, MailingService>();
            services.AddScoped<IManagementService, ManagementService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentRepo, DocumentRepo>();
            services.AddScoped<IPaymentRepo,PaymentRepo>();
            services.AddScoped<IpaymentService, PaymentService>();
            services.AddScoped<HashIdService>();

        }
    }
}

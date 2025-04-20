
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StudentGuide.API;
using StudentGuide.BLL.Services.Materials;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.UnitOfWork;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace StudentGuide;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });
        builder.Services.AddControllers();
        // Configure Hashids
        builder.Services.AddSingleton<IHashids>(_ => new Hashids("135790$$$%%#^#2^f", 8));
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services
       .AddGraphQLServer()
       .AddQueryType<Query>();
        builder.Services.AddControllers()
       .AddJsonOptions(o =>
           o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        #region Database
        builder.Services.AddDbContext<ApplicationDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("connection"),
         b => b.MigrationsAssembly("StudentGuide.DAL"))
 );


        #endregion
        #region Services
        builder.Services.RegisterServices();
        #endregion
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.WithTheme(ScalarTheme.Moon)
                .WithDarkMode(true)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                .WithDarkModeToggle(false)
                .WithPreferredScheme("Bearer")
                .WithHttpBearerAuthentication(bearer =>
                {
                    bearer.Token = "your-bearer-token";
                });
            options.Authentication = new ScalarAuthenticationOptions
            {
                PreferredSecurityScheme = "Bearer"
            };
        });


        builder.Services.AddEndpointsApiExplorer();

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Models;
namespace StudentGuide.DAL.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Student> Stduents { get; set; }
        public DbSet<Document> Documents { get; set; }
         //add Auth Authrize
         //add logout
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new {sc.StudentId,sc.CourseCode});
            #region Course
            modelBuilder.Entity<Course>(entity =>

            {
                entity.HasKey(c => c.Code);

                entity.HasMany(c => c.CourseDepartments)
                      .WithOne(cd => cd.Course)
                      .HasForeignKey(cd => cd.CoursesCode);

            });
            #endregion

            #region CourseDepartment
            modelBuilder.Entity<CourseDepartment>()
                .HasKey(cd => new { cd.CoursesCode, cd.DepartmentsCode });

            modelBuilder.Entity<CourseDepartment>()
                .HasOne(cd => cd.Course)
                .WithMany(c => c.CourseDepartments)
                .HasForeignKey(cd => cd.CoursesCode);

            modelBuilder.Entity<CourseDepartment>()
                .HasOne(cd => cd.Department)
                .WithMany(d => d.CourseDepartments)
                .HasForeignKey(cd => cd.DepartmentsCode);
            #endregion

            #region Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Code);

                entity.HasMany(d => d.CourseDepartments)
                      .WithOne(cd => cd.Department)
                      .HasForeignKey(cd => cd.DepartmentsCode);
            });
            #endregion

            modelBuilder.Entity<Material>()
                .HasIndex(m => m.Name)
                .HasDatabaseName("IX_Material_Name");
            modelBuilder.Entity<ApplicationUser>()
            .HasOne(a => a.Student)
            .WithOne(s => s.ApplicationUser)
            .HasForeignKey<ApplicationUser>(a => a.StudentCode);
            base.OnModelCreating(modelBuilder);
          
        }
    }
}

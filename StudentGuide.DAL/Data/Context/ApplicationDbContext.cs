using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Student> Stduents { get; set; }
        //payment
        //jwt
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
            base.OnModelCreating(modelBuilder);
        }
    }
}

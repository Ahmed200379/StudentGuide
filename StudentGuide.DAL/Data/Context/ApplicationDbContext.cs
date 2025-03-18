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
        public DbSet<Stduent> Stduents { get; set; }
        //material
        //deparament
        //Course
        //payment
        //student
        //jwt

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Code);

                entity.HasMany(c => c.Departments)
                .WithMany(d => d.Courses);

                entity.HasMany(c => c.Stduents)
                .WithMany(m => m.Courses);

            });
            #endregion
            #region Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(c => c.Code);

                entity.HasMany(d => d.Courses)
                .WithMany(s => s.Departments);
            }
            );
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}

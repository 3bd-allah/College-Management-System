using CollegeManagementSystem.Models.Entities;
using CollegeManagementSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Models.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, int>
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Enrollment> Enrollments  { get; set; }
        public virtual DbSet<GradeYear> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.; Initial Catalog=CollegeDB; Integrated Security=True; TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}

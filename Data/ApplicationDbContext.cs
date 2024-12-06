using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkToDo.Models;

namespace WorkToDo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        //public DbSet<ApplicationUser> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.TaskId); // Explicitly set TaskId as the primary key
            });

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Work", Description = "Work Description" },
                new Category { CategoryId = 2, Name = "Personal", Description = "Personal Description" }
            );
        }

    }
}

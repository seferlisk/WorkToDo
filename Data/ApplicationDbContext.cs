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
        public DbSet<WorkItem> WorkItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure reverse navigation properties
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.WorkItem)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.WorkItemId);


            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.Category)
                .WithMany(c => c.WorkItems)
                .HasForeignKey(w => w.CategoryId);

            modelBuilder.Entity<Comment>()
                .HasOne(w => w.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(w => w.UserId);
        }

    }
}

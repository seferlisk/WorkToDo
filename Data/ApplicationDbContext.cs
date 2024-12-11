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

            //modelBuilder.Entity<Comment>(entity =>
            //{
            //    entity.HasKey(e => e.CommentId); // Primary Key
            //    entity.HasOne(e => e.WorkItem)   // Relationship with WorkItem
            //          .WithMany(w => w.Comments) // Assuming `WorkItem` has `ICollection<Comment>`
            //          .HasForeignKey(e => e.WorkItemId)
            //          .OnDelete(DeleteBehavior.Cascade);

            //    entity.HasOne(e => e.User)       // Relationship with User
            //          .WithMany()                // Adjust if User has a collection of Comments
            //          .HasForeignKey(e => e.UserId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});
        }

    }
}

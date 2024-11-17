using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorkToDo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Category { get; set; }
        public DbSet<Task> Comment { get; set; }
        public DbSet<Task> ErrorViewModel { get; set; }
        public DbSet<Task> Project { get; set; }

        public DbSet<Task> Task { get; set; }
        public DbSet<Task> User { get; set; }
    }
}

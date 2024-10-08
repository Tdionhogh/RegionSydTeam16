using Microsoft.EntityFrameworkCore;
using RegionSyd.Model;

namespace RegionSyd.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CustomTask> CustomTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomTask>()
                .ToTable("Task"); // Maps CustomTask to the Task table in the database
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(App.ConnectionString);
        }
    }
}
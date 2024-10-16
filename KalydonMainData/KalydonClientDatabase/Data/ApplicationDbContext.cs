using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KalydonClientDatabase.Models;

namespace KalydonClientDatabase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<KalydonClientDatabase.Models.Client> Client { get; set; }
        public DbSet<KalydonClientDatabase.Models.Project> Project { get; set; }
        public DbSet<KalydonClientDatabase.Models.ProjectTask> ProjectTask { get; set; }
        public DbSet<KalydonClientDatabase.Models.Meeting> Meeting { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string newConnString = "Data Source=" + Path.Join(Directory.GetCurrentDirectory(), "KalydonData.db");
            //optionsBuilder.UseSqlite(newConnString);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
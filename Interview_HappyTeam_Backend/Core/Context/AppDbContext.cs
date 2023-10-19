using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Interview_HappyTeam_Backend.Core.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Config> Configs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Config>();

            modelBuilder.Entity<ErrorLog>();

            modelBuilder.Entity<Order>()
                .Property(order => order.Status)
                .HasConversion<string>();
        }
    }
}

using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Interview_HappyTeam_Backend.Core.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
                                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasOne(car => car.CarModel)
                .WithMany(carModel => carModel.Cars)
                .HasForeignKey(car => car.CarModelId);

            modelBuilder.Entity<Car>()
                .HasOne(car => car.Order)
                .WithOne(order => order.Car)
                .HasForeignKey<Car>(car => car.OrderId);

            modelBuilder.Entity<Location>()
                .HasOne(location => location.Country)
                .WithMany(country => country.Locations)
                .HasForeignKey(location => location.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Client)
                .WithMany(client => client.Orders)
                .HasForeignKey(order => order.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
               .HasOne(order => order.LocationStart)
               .WithMany()
               .HasForeignKey(order => order.LocationIdStart)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
               .HasOne(order => order.LocationEnd)
               .WithMany()
               .HasForeignKey(order => order.LocationIdEnd)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Car)
                .WithOne(car => car.Order)
                .HasForeignKey<Order>(order => order.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Country)
                .WithMany()
                .HasForeignKey(order => order.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .Property(order => order.Status)
                .HasConversion<string>();
        }
    }
}

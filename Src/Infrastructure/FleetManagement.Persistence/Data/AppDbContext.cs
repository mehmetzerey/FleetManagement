using System.Reflection;
using FleetManagement.Domain.Entities;

namespace FleetManagement.Persistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {
    }

    public DbSet<Bag> Bags => Set<Bag>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<DeliveryPoint> DeliveryPoints => Set<DeliveryPoint>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<PackagesToBags> PackagesToBags => Set<PackagesToBags>();
    public DbSet<WrongDeliveryLog> WrongDeliveryLog => Set<WrongDeliveryLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

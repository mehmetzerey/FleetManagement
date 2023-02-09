

namespace FleetManagement.Persistence.Data;

public class AppDbContextSeeds
{

    public static async Task SeedAsync(AppDbContext context,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (!await context.Vehicles.AnyAsync())
            {
                await context.Vehicles.AddRangeAsync(
                    GetPreconfiguredVehicle());

                await context.SaveChangesAsync();
            }

            if (!await context.Bags.AnyAsync())
            {
                await context.Bags.AddRangeAsync(
                    GetPreconfiguredBags());

                await context.SaveChangesAsync();
            }

            if (!await context.DeliveryPoints.AnyAsync())
            {
                await context.DeliveryPoints.AddRangeAsync(
                    GetPreconfiguredDeliveryPoint());

                await context.SaveChangesAsync();
            }

            if (!await context.Packages.AnyAsync())
            {
                await context.Packages.AddRangeAsync(
                    GetPreconfiguredPackage());

                await context.SaveChangesAsync();
            }

            if (!await context.PackagesToBags.AnyAsync())
            {
                foreach (var packagesToBags in GetPreconfiguredPackagesToBags())
                {
                    var getPackage = await context.Packages.FindAsync(packagesToBags.PackageId) ?? new Package();
                    var getBag = await context.Bags.FindAsync(packagesToBags.BagId) ?? new Bag();
                    getPackage.UpdatePackageStatus(Domain.Enums.PackageStatus.LoadedIntoBag);
                    getBag.UpdateBagStatus(Domain.Enums.BagStatus.Created);

                }
                await context.PackagesToBags.AddRangeAsync(
                    GetPreconfiguredPackagesToBags());

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedAsync(context, logger, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<Bag> GetPreconfiguredBags()
    {
        return new List<Bag>
        {
            new Bag("C725799", "2"),
            new Bag("C725800", "3")
        };
    }
    static IEnumerable<Vehicle> GetPreconfiguredVehicle()
    {
        return new List<Vehicle>
        {
            new Vehicle("34 TL 34")
        };
    }
    static IEnumerable<DeliveryPoint> GetPreconfiguredDeliveryPoint()
    {
        return new List<DeliveryPoint>
        {
            new DeliveryPoint( "Branch",  "1"),
            new DeliveryPoint( "Distribution Center", "2"),
            new DeliveryPoint( "Transfer Center", "3")
        };
    }
    static IEnumerable<PackagesToBags> GetPreconfiguredPackagesToBags()
    {
        return new List<PackagesToBags>
        {
            new PackagesToBags(){ PackageId = "P8988000122", BagId = "C725799"},
            new PackagesToBags(){PackageId = "P8988000126", BagId = "C725799"},
            new PackagesToBags(){PackageId = "P9988000128", BagId = "C725800"},
            new PackagesToBags(){PackageId = "P9988000129", BagId = "C725800"},
        };
    }
    static IEnumerable<Package> GetPreconfiguredPackage()
    {
        return new List<Package>
        {
            new Package("P7988000121", "1", 5),
            new Package("P7988000122", "1", 5),
            new Package("P7988000123", "1", 9),
            new Package("P8988000120", "2", 33),
            new Package("P8988000121", "2", 17),
            new Package("P8988000122", "2", 26),
            new Package("P8988000123", "2", 35),
            new Package("P8988000124", "2", 1),
            new Package("P8988000125", "2", 200),
            new Package("P8988000126", "2", 50),
            new Package("P9988000126", "3", 15),
            new Package("P9988000127", "3", 16),
            new Package("P9988000128", "3", 55),
            new Package("P9988000129", "3", 28),
            new Package("P9988000130", "3", 17),
        };
    }

}

using FleetManagement.Application.Interfaces.Repositories.Specifications;
using FleetManagement.Application.Interfaces.Repositories.WrongDeliveryLog;
using FleetManagement.Persistence.Services.Repositories.Specifications;
using FleetManagement.Persistence.Services.Repositories.WrongDeliveryLog;

namespace FleetManagement.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(IConfiguration configuration, IServiceCollection services)
    {
        var useOnlyInMemoryDatabase = false;
        if (configuration["UseOnlyInMemoryDatabase"] != null)
        {
            useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]);
        }

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<AppDbContext>(c =>
               c.UseInMemoryDatabase("FleetManagement"));

        }
        else
        {
            // use memory database
            services.AddDbContext<AppDbContext>(c =>
                c.UseInMemoryDatabase("FleetManagement"));
        }

        services.AddScoped<IReadBagRepository, ReadBagRepository>();
        services.AddScoped<IWriteBagRepository, WriteBagRepository>();

        services.AddScoped<IReadPackageRepository, ReadPackageRepository>();
        services.AddScoped<IWritePackageRepository, WritePackageRepository>();

        services.AddScoped<IReadPackagesToBagsRepository, ReadPackagesToBagsRepository>();
        services.AddScoped<IWritePackagesToBagsRepository, WritePackagesToBagsRepository>();

        services.AddScoped<IReadDeliveryPointRepository, ReadDeliveryPointRepository>();
        services.AddScoped<IWriteDeliveryPointRepository, WriteDeliveryPointRepository>();

        services.AddScoped<IReadVehicleRepository, ReadVehicleRepository>();
        services.AddScoped<IWriteVehicleRepository, WriteVehicleRepository>();

        services.AddScoped<IReadWrongDeliveryLogRepository, ReadWrongDeliveryLogRepository>();
        services.AddScoped<IWriteWrongDeliveryLogRepository, WriteWrongDeliveryLogRepository>();

        services.AddScoped<IBagWithPackagesToBagsSpecification, BagWithPackagesToBagsSpecification>();
        services.AddScoped<IPackageWithPackagesToBagsSpecification, PackageWithPackagesToBagsSpecification>();
    }
}

namespace FleetManagement.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
    }
}

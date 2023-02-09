namespace FleetManagement.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices( IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistration));
    }
}

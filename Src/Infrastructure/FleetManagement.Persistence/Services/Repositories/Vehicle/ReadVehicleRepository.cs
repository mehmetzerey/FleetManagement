namespace FleetManagement.Persistence.Services.Repositories.Vehicle;

public class ReadVehicleRepository : ReadRepository<Entity.Vehicle>, IReadVehicleRepository
{
    public ReadVehicleRepository(AppDbContext context) : base(context)
    {
    }
}

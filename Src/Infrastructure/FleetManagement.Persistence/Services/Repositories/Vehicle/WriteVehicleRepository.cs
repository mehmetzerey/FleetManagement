namespace FleetManagement.Persistence.Services.Repositories.Vehicle;

public class WriteVehicleRepository : WriteRepository<Entity.Vehicle>, IWriteVehicleRepository
{
    public WriteVehicleRepository(AppDbContext context) : base(context)
    {
    }
}

namespace FleetManagement.Persistence.Services.Repositories.DeliveryPoint;

public class WriteDeliveryPointRepository : WriteRepository<Entity.DeliveryPoint>, IWriteDeliveryPointRepository
{
    public WriteDeliveryPointRepository(AppDbContext context) : base(context)
    {
    }
}

namespace FleetManagement.Persistence.Services.Repositories.DeliveryPoint;

public class ReadDeliveryPointRepository : ReadRepository<Entity.DeliveryPoint>, IReadDeliveryPointRepository
{
    public ReadDeliveryPointRepository(AppDbContext context) : base(context)
    {
    }
}

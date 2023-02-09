using FleetManagement.Application.Interfaces.Repositories.WrongDeliveryLog;

namespace FleetManagement.Persistence.Services.Repositories.WrongDeliveryLog;

public class ReadWrongDeliveryLogRepository : ReadRepository<Entity.WrongDeliveryLog>, IReadWrongDeliveryLogRepository
{
    public ReadWrongDeliveryLogRepository(AppDbContext context) : base(context)
    {
    }
}

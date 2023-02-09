using FleetManagement.Application.Interfaces.Repositories.WrongDeliveryLog;

namespace FleetManagement.Persistence.Services.Repositories.WrongDeliveryLog;

public class WriteWrongDeliveryLogRepository : WriteRepository<Entity.WrongDeliveryLog>, IWriteWrongDeliveryLogRepository
{
    public WriteWrongDeliveryLogRepository(AppDbContext context) : base(context)
    {
    }
}

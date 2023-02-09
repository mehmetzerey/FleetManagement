using FleetManagement.Application.Interfaces.Repositories.Bag;
using FleetManagement.Persistence.Data;

namespace FleetManagement.Persistence.Services.Repositories.Bag;

public class ReadBagRepository : ReadRepository<Entity.Bag>, IReadBagRepository
{
    public ReadBagRepository(AppDbContext context) : base(context)
    {
    }
}

namespace FleetManagement.Persistence.Services.Repositories.Bag;

public class WriteBagRepository : WriteRepository<Entity.Bag>, IWriteBagRepository
{
    public WriteBagRepository(AppDbContext context) : base(context)
    {
    }
}

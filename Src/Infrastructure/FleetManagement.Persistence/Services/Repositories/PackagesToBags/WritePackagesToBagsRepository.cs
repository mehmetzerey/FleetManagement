namespace FleetManagement.Persistence.Services.Repositories.PackagesToBags;

public class WritePackagesToBagsRepository : WriteRepository<Entity.PackagesToBags>, IWritePackagesToBagsRepository
{
    public WritePackagesToBagsRepository(AppDbContext context) : base(context)
    {
    }
}

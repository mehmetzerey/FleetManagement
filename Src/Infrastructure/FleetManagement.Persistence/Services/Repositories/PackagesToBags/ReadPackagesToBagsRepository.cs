namespace FleetManagement.Persistence.Services.Repositories.PackagesToBags;

public class ReadPackagesToBagsRepository : ReadRepository<Entity.PackagesToBags>, IReadPackagesToBagsRepository
{
    public ReadPackagesToBagsRepository(AppDbContext context) : base(context)
    {
    }
}

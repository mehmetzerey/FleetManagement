namespace FleetManagement.Persistence.Services.Repositories.Package;

public class ReadPackageRepository : ReadRepository<Entity.Package>, IReadPackageRepository
{
    public ReadPackageRepository(AppDbContext context) : base(context)
    {
    }
}

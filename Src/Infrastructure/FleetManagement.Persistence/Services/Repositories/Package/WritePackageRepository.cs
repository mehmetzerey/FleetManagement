namespace FleetManagement.Persistence.Services.Repositories.Package;

public class WritePackageRepository : WriteRepository<Entity.Package>, IWritePackageRepository
{
    public WritePackageRepository(AppDbContext context) : base(context)
    {
    }
}

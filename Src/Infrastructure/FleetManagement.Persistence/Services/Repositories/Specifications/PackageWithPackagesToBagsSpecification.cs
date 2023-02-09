using FleetManagement.Application.Interfaces.Repositories.Specifications;

namespace FleetManagement.Persistence.Services.Repositories.Specifications;

public class PackageWithPackagesToBagsSpecification : IPackageWithPackagesToBagsSpecification
{
    private readonly AppDbContext _context;

    public PackageWithPackagesToBagsSpecification(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<Domain.Entities.Package> Table => _context.Packages;

    public IQueryable<Domain.Entities.Package> GetPackage(string Id)
    {
        return Table.Where(x => x.Id == Id).Include(x => x.PackagesToBags).ThenInclude(x => x.Bag);
    }
}

namespace FleetManagement.Application.Interfaces.Repositories.Specifications;

public interface IPackageWithPackagesToBagsSpecification : IRepository<Entity.Package>
{
    IQueryable<Entity.Package> GetPackage(string Id);
}

namespace FleetManagement.Application.Interfaces.Repositories.Specifications;

public interface IBagWithPackagesToBagsSpecification : IRepository<Entity.Bag>
{
    IQueryable<Entity.Bag> Bags(string Barcode);
}

using System.Linq.Expressions;
using FleetManagement.Application.Interfaces.Repositories.Specifications;

namespace FleetManagement.Persistence.Services.Repositories.Specifications;

public class BagWithPackagesToBagsSpecification : IBagWithPackagesToBagsSpecification
{
    private readonly AppDbContext _context;

    public BagWithPackagesToBagsSpecification(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<Domain.Entities.Bag> Table => _context.Bags;

    public IQueryable<Domain.Entities.Bag> Bags(string Barcode)
    {
        return Table.Where(x=> x.Id == Barcode).Include(x=> x.PackagesToBags).ThenInclude(y=> y.Package);
    }
}

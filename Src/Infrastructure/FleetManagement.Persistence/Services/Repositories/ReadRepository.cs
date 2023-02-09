using Ardalis.GuardClauses;
using System.Linq.Expressions;
using FleetManagement.Application.Interfaces.Repositories;
using FleetManagement.Persistence.Data;

namespace FleetManagement.Persistence.Services.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public async Task<T> GetByIdAsync(string Id) => await Table.FindAsync(Id);

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        Guard.Against.Null(method, nameof(method));
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        Guard.Against.Null(method, nameof(method));
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
}

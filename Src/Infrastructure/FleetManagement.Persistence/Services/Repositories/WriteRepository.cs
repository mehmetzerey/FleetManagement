using Microsoft.EntityFrameworkCore.ChangeTracking;
using FleetManagement.Persistence.Data;

namespace FleetManagement.Persistence.Services.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added; ;
    }

    public async Task<bool> AddRangeAsync(List<T> model)
    {
        await Table.AddRangeAsync(model);
        return true;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> Remove(int Id)
    {
        T model = await Table.FindAsync(Id);
        return Remove(model);
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public bool Update(T model)
    {
        EntityEntry<T> entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }
}

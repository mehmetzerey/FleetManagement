namespace FleetManagement.Application.Interfaces.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class
{
    Task<bool> AddAsync(T model);
    Task<bool> AddRangeAsync(List<T> model);
    bool Remove(T model);
    Task<bool> Remove(int Id);
    bool Update(T model);
    Task<int> SaveAsync();
}

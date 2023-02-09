namespace FleetManagement.Application.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    DbSet<T> Table { get; }
}

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Services.DataBaseServices;

public abstract class DataServiceBase<T> : IDataServices<T> where T : class, new()
{
    protected IUnitOfWork UnitOfWork { get; set; }
    protected DataServiceBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    public abstract Task<T> GetOneAsync(dynamic? key);
    public abstract Task AddAsync(T entity);
    public abstract Task UpdateAsync(T entity);
    public abstract Task DeleteAsync(T entity);
    public abstract Task DeleteAsyncID(dynamic key);
    public abstract Task<IList<T>> GetAllAsync();
}

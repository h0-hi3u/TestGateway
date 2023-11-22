using System.Linq.Expressions;

namespace Application.Interfaces.Services;

public interface IDataServices<T> where T : class, new()
{
    Task<IList<T>> GetAllAsync();
    Task<T> GetOneAsync(dynamic? key);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAsyncID(dynamic? key);
}

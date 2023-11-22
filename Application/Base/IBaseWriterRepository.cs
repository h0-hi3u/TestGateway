namespace Application.Base;

public interface IBaseWriterRepository<T> where T : class
{
    Task<T> InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAsyncID(dynamic key);
}

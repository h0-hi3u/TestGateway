namespace Application.Base;

public interface IBaseReaderRepository <T> where T : class
{
    Task<List<T>> GetAllAsync();
    T Find(dynamic key);
    Task<T> FindAsync(dynamic key);
}

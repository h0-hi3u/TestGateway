using Application.Base;

namespace Application.Interfaces.Repositories;

public interface IRepository<T> : IBaseReaderRepository<T>, IBaseWriterRepository<T> where T : class
{

}

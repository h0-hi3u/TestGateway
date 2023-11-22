using Model.EntityModels;

namespace Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    TestGatewayContext _context { get; }
    IRepository<T> Repository<T>() where T : class;
}

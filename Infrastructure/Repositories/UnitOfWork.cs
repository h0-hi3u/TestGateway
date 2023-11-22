using Application.Interfaces.Repositories;
using Model.EntityModels;

namespace Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private Dictionary<string, object> Repositories {  get; }
    public TestGatewayContext _context { get; set; }

    public UnitOfWork(TestGatewayContext context)
    {
        _context = context;
        Repositories = new Dictionary<string, object>();
    }

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        var typeName = type.Name;

        lock (Repositories)
        {
            if (Repositories.ContainsKey(typeName))
            {
                return (IRepository<T>)Repositories[typeName];
            }
            var repository = new Repository<T>(_context);

            Repositories.Add(typeName, repository);
            return repository;
        }
    }
}

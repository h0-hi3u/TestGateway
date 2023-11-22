using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Model.EntityModels;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    public TestGatewayContext _context { get; private set; }

    public Repository(TestGatewayContext context)
    {
        _context = context;
    }

    public DbSet<T> Entities => _context.Set<T>();
    

    public async Task DeleteAsync(T entity)
    {
        Entities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsyncID(dynamic key)
    {
        var entity = await Entities.FindAsync(key);
        if (entity != null) {
           await DeleteAsync(entity);
           await _context.SaveChangesAsync();
        }
    }

    public T Find(dynamic key)
    {
        return Entities.Find(key);
    }

    public async Task<T> FindAsync(dynamic key)
    {
        return await Entities.FindAsync(key);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Entities.ToListAsync(); 
    }

    public async Task<T> InsertAsync(T entity)
    {
        await Entities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        Entities.Update(entity);
        await _context.SaveChangesAsync();
    }
}

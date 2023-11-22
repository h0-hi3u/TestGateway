using Application.Interfaces.Services;
using Infrastructure.Repositories;
using Services.DataBaseServices;
using Model.EntityModels;
using System.Linq.Expressions;
using Application.Interfaces.Repositories;

namespace Services.Services;

public class ProductServices : DataServiceBase<Product>, IProductServices
{
    public ProductServices(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }
    public override async Task AddAsync(Product entity)
    {
        await UnitOfWork.Repository<Product>().InsertAsync(entity);
    }

    public override async Task DeleteAsync(Product entity)
    {
        await UnitOfWork.Repository<Product>().DeleteAsync(entity);
    }

    public override async Task DeleteAsyncID(dynamic key)
    {
        await UnitOfWork.Repository<Product>().DeleteAsyncID(key);
    }

    public override async Task<IList<Product>> GetAllAsync()
    {
        return await UnitOfWork.Repository<Product>().GetAllAsync();
    }

    public override async Task<Product> GetOneAsync(dynamic? key)
    {
        return await UnitOfWork.Repository<Product>().FindAsync(key);
    }

    public override async Task UpdateAsync(Product entity)
    {
        await UnitOfWork.Repository<Product>().UpdateAsync(entity);
    }
}

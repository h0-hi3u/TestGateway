using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Model.EntityModels;
using Services.DataBaseServices;

namespace Services.Services;

public class SupplierServices : DataServiceBase<Supplier>, ISupplierServices
{
    public SupplierServices(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task AddAsync(Supplier entity)
    {
        await UnitOfWork.Repository<Supplier>().InsertAsync(entity);
    }

    public override async Task DeleteAsync(Supplier entity)
    {
        await UnitOfWork.Repository<Supplier>().DeleteAsync(entity);
    }

    public override async Task DeleteAsyncID(dynamic key)
    {
        await UnitOfWork.Repository<Supplier>().DeleteAsyncID(key);
    }

    public override async Task<IList<Supplier>> GetAllAsync()
    {
        return await UnitOfWork.Repository<Supplier>().GetAllAsync(); 
    }

    public override async Task<Supplier> GetOneAsync(dynamic? key)
    {
        return await UnitOfWork.Repository<Supplier>().FindAsync(key);
    }

    public override async Task UpdateAsync(Supplier entity)
    {
        await UnitOfWork.Repository<Supplier>().UpdateAsync(entity);
    }
}


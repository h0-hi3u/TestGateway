using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Model.EntityModels;
using Services.DataBaseServices;

namespace Services.Services;
public class CategoryServices : DataServiceBase<Category>, ICategoryServices
{
    public CategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task AddAsync(Category entity)
    {
        await UnitOfWork.Repository<Category>().InsertAsync(entity);
    }

    public override async Task DeleteAsync(Category entity)
    {
        await UnitOfWork.Repository<Category>().DeleteAsync(entity);
    }

    public override async Task DeleteAsyncID(dynamic key)
    {
        await UnitOfWork.Repository<Category>().DeleteAsyncID(key);
    }

    public override async Task<IList<Category>> GetAllAsync()
    {
        return await UnitOfWork.Repository<Category>().GetAllAsync();
    }

    public override async Task<Category> GetOneAsync(dynamic? key)
    {
        return await UnitOfWork.Repository<Category>().FindAsync(key);
    }

    public override async Task UpdateAsync(Category entity)
    {
        await UnitOfWork.Repository<Category>().UpdateAsync(entity);
    }
}

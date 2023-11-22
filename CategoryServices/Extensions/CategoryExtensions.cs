using Model.DTO.Category;
using Model.EntityModels;

namespace CategoryServices.Extensions;

public static class CategoryExtensions
{
    public static List<DtoCategory> WithoutProduct(this IList<Category> categories)
    {
        return (List<DtoCategory>)categories.Select(c => new DtoCategory
        {
           Id = c.CategoryId,
           Name = c.CategoryName,
           Description = c.Description
        });
    }
}

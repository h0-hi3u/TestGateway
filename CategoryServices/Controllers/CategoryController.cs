using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Category;
using Model.EntityModels;

namespace CategoryServices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryServices _categoryServices;

    public CategoryController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _categoryServices.GetAllAsync();
    }
    [HttpGet("{id}", Name = nameof(GetCategoryById))]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetCategoryById(string? id)
    {
        Category? c = await _categoryServices.GetOneAsync(id);
        if (c == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(c);
        }
    }
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Creat([FromBody] DtoCategory dtoCategory)
    {
        try
        {
            if (dtoCategory == null)
            {
                return BadRequest();
            }
            Category category = new Category
            {
                CategoryId = dtoCategory.Id,
                CategoryName = dtoCategory.Name,
                Description = dtoCategory.Description,
            };
            await _categoryServices.AddAsync(category);
            return CreatedAtRoute(
                routeName: nameof(GetCategoryById),
                routeValues: new { id = category.CategoryId },
                value: category
                );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string? id, [FromBody] DtoCategory dtoCategory)
    {
        if (dtoCategory == null || id != dtoCategory.Id)
        {
            return BadRequest();
        }
        Category? category = await _categoryServices.GetOneAsync(id);
        if(category == null)
        {
            return NotFound();
        } else
        {
            category.CategoryName = dtoCategory.Name;
            category.Description = dtoCategory.Description;
        }
        await _categoryServices.UpdateAsync(category);
        return NoContent();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string? id)
    {
        try
        {
            Category? exsiting = await _categoryServices.GetOneAsync(id);
            if (exsiting == null)
            {
                ProblemDetails problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "",
                    Title = $"Delete category id {id}",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problemDetails);
            }
            await _categoryServices.DeleteAsync(exsiting);
            return NoContent();
        }catch (Exception ex) {
            return BadRequest($"Exception at delete category {id}");
        }
    }
}

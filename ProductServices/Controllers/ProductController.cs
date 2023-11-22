using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Product;
using Model.EntityModels;

namespace ProductServices.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productservices;

    public ProductController(IProductServices productservices)
    {
        _productservices = productservices;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productservices.GetAllAsync();
    }

    [HttpGet("{id}", Name = nameof(GetProductById))]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetProductById(string id)
    {
        Product p = await _productservices.GetOneAsync(id);
        if (p != null)
        {
            return Ok(p);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Product))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] DtoCreateProduct dtoCreateProduct)
    {
        try
        {
            if (dtoCreateProduct == null)
            {
                return BadRequest();
            }
            Product p = new Product
            {
                ProductId = dtoCreateProduct.Id,
                ProductName = dtoCreateProduct.Name,
                Quantity = dtoCreateProduct.Quantity,
                Price = dtoCreateProduct.Price,
                SupplierId = dtoCreateProduct.SupplierId,
                CategoryId = dtoCreateProduct.CategoryId
            };
            await _productservices.AddAsync(p);
            return CreatedAtRoute(
                routeName: nameof(GetProductById),
                routeValues: new
                {
                    id = p.ProductId,
                },
                value: p);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(string? id, [FromBody] DtoCreateProduct dtoCreateProduct)
    {

        if (dtoCreateProduct == null || dtoCreateProduct.Id != id)
        {
            return BadRequest();
        }
        Product? exsiting = await _productservices.GetOneAsync(id);
        if (exsiting == null)
        {
            return NotFound();
        } else
        {
            exsiting.ProductName = dtoCreateProduct.Name;
            exsiting.Quantity = dtoCreateProduct.Quantity;
            exsiting.Price = dtoCreateProduct.Price;
            exsiting.SupplierId = dtoCreateProduct.SupplierId;
            exsiting.CategoryId = dtoCreateProduct.CategoryId;
        }
        await _productservices.UpdateAsync(exsiting);
        return new NoContentResult();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Delete(string? id)
    {
        try
        {
            Product? exsiting = await _productservices.GetOneAsync(id);
            if(exsiting == null)
            {
                ProblemDetails problem = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://7040/delete-product",
                    Title = $"Error for delete {id}",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problem);
            }
            await _productservices.DeleteAsyncID(id);
            return new NoContentResult();
        } catch(Exception ex)
        {
            return BadRequest($"Exception at delete product {id}");
        }
    }
}


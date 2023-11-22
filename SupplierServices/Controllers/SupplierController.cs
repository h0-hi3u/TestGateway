using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Supplier;
using Model.EntityModels;

namespace SupplierServices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierServices _supplierServices;
    public SupplierController(ISupplierServices supplierServices)
    {
        _supplierServices = supplierServices;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Supplier>))]
    public async Task<IEnumerable<Supplier>> GetAll()
    {
        return await _supplierServices.GetAllAsync();
    }

    [HttpGet("{id}",Name = nameof(GetSupplierById))]
    [ProducesResponseType(200, Type = typeof(Supplier))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetSupplierById(string id)
    {
        Supplier s = await _supplierServices.GetOneAsync(id);
        if (s != null)
        {
            return Ok(s);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] DtoSupplier dtoSupplier)
    {
        try
        {
            Supplier s = new Supplier
            {
                SupplierId = dtoSupplier.Id,
                SupplierName = dtoSupplier.Name,
                City = dtoSupplier.City,
                Country = dtoSupplier.Country,
            };
            if(s == null)
            {
                return BadRequest();
            } 
            await _supplierServices.AddAsync(s);
            return CreatedAtRoute(
                routeName: nameof(GetSupplierById),
                routeValues: new { id = s.SupplierId },
                value: s
                );
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string? id, [FromBody] DtoSupplier dtoSupplier)
    {
        if(dtoSupplier == null || dtoSupplier.Id != id)
        {
            return BadRequest();
        }
        Supplier? supplier = await _supplierServices.GetOneAsync(id);
        if(supplier == null)
        {
            return NotFound();
        } else
        {
            supplier.SupplierName = dtoSupplier.Name;
            supplier.City = dtoSupplier.City;
            supplier.Country = dtoSupplier.Country;
        }
        await _supplierServices.UpdateAsync(supplier);
        return NoContent();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string? id)
    {
        try
        {
            Supplier? exsiting = await _supplierServices.GetOneAsync(id);
            if(exsiting == null)
            {
                ProblemDetails problem = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "   ",
                    Title = $"Error delete supplier {id}",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problem);
            }
            await _supplierServices.DeleteAsyncID(id);
            return NoContent();
        } catch (Exception)
        {
            return BadRequest($"Exception at delete supplier {id}");
        }
    }
}

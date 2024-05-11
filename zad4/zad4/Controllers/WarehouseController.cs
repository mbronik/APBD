using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using zad4.DTO;
using zad4.Model;
using zad4.Service;

namespace zad4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private WarehouseService _warehouseService;

    public WarehouseController(WarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    [HttpPost]
    public IActionResult AddProductToWarehouse(ProductWarehouseDTO productWarehouseDto)
    {
        try
        {
            var id = _warehouseService.AddProductToWarehouse(productWarehouseDto);
            return StatusCode(StatusCodes.Status201Created, id);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}
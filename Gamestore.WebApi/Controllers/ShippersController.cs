using Gamestore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ShippersController(IShipperService shipperService) : ControllerBase
{
    // GET: shippers
    [HttpGet]
    public async Task<IActionResult> GeShippersAsync()
    {
        var shippers = await shipperService.GetAllShippersAsync();

        return Ok(shippers);
    }
}

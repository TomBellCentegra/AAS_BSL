using AAS_BSL.Domain.Canonical;
using AAS_BSL.Services.Order;
using Microsoft.AspNetCore.Mvc;

namespace AAS_BSL.Controllers;

[ApiController]
[Produces("application/json")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("bsl/canonical")]
    public async Task<IActionResult> ReceiveOrder(Canonical request)
    {
        try
        {
            await _orderService.Process(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
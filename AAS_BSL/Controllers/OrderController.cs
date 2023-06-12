using AAS_BSL.Domain.Canonical;
using AAS_BSL.Domain.Logger;
using AAS_BSL.Extensions;
using AAS_BSL.Services.Logger;
using AAS_BSL.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AAS_BSL.Controllers;

[ApiController]
[Produces("application/json")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILoggerService _loggerService;

    public OrderController(
        IOrderService orderService,
        ILoggerService loggerService)
    {
        _orderService = orderService;
        _loggerService = loggerService;
    }

    [HttpPost]
    [Route("bsl/canonical")]
    public async Task<IActionResult> ReceiveOrder()
    {
        try
        {
            string body = await Request.GetRawBodyAsync();

            await _loggerService.Save(new Log("test", body));
            
            Canonical myDeliverooOrder = JsonConvert.DeserializeObject<Canonical>(body);


            await _orderService.Process(myDeliverooOrder);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
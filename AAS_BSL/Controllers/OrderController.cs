using AAS_BSL.Domain.Canonical;
using AAS_BSL.Domain.Dtos;
using AAS_BSL.Domain.Logger;
using AAS_BSL.Services.HttpClient;
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
    private readonly IBslHttpClient _httpClient;

    public OrderController(
        IOrderService orderService,
        ILoggerService loggerService,
        IBslHttpClient httpClient)
    {
        _orderService = orderService;
        _loggerService = loggerService;
        _httpClient = httpClient;
    }

    [HttpPost]
    [Route("bsl/canonical")]
    public async Task<IActionResult> ReceiveOrder(Request request)
    {
        try
        {
            var disc = request.attributes.FirstOrDefault(x => x.Key == "tlog_id");

            await _loggerService.Save(new Log(disc.Value, JsonConvert.SerializeObject(request)));

            var res = await _httpClient.GetTransactionLog(disc.Value);

            var canonical = JsonConvert.DeserializeObject<Canonical>(res.Message);

            await _orderService.Process(canonical);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
using AAS_BSL.Domain.Canonical;
using AAS_BSL.Domain.Dtos;
using AAS_BSL.Domain.Enums;
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
            var disc = request.attributes.Select(x => new { x.Key, x.Value }).ToDictionary(x => x.Key, x => x.Value);


            string tlogId, subtype;
            disc.TryGetValue("tlog_id", out tlogId);
            disc.TryGetValue("subtype", out subtype);

            await _loggerService.Save(new Log(tlogId, JsonConvert.SerializeObject(request)));

            if (!string.IsNullOrEmpty(subtype) && subtype.Equals("RETURN", StringComparison.OrdinalIgnoreCase))
            {
                await _orderService.ProcessCancellation(tlogId);

                return Ok("Cancellation end successfully");
            }

            var res = await _httpClient.GetTransactionLog(tlogId);

            if (res.Status == Status.Failed)
            {
                await _loggerService.Save(new Log(tlogId, res.Message));

                return BadRequest(res.Message);
            }

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
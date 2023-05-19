using AAS_BSL.Domain.Dtos;
using AAS_BSL.Services.Subsription;
using Microsoft.AspNetCore.Mvc;

namespace AAS_BSL.Controllers;

[ApiController]
[Produces("application/json")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    [Route("bsl/subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequestDto companyRequest)
    {
        try
        {
            var result = await _subscriptionService.Process(companyRequest);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
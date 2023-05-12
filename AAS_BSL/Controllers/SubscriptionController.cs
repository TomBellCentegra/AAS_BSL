using AAS_BSL.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AAS_BSL.Controllers;

[ApiController]
[Produces("application/json")]
public class SubscriptionController : ControllerBase
{
    [HttpPost]
    [Route("bsl/subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest orderRequest)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
using BrawlhallaStat.Api.BrawlhallaEntities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers.BrawlhallaEntities;

[Route("api/[controller]/[action]")]
public class WeaponController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeaponController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddWeaponQuery request)
    {
        var id = await _mediator.Send(request);
        return Ok(id);
    }
}
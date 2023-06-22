using BrawlhallaStat.Api.Commands.BrawlhallaEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers.BrawlhallaEntities;

[Route("api/[controller]/[action]")]
public class LegendsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LegendsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddLegend request)
    {
        var id = await _mediator.Send(request);
        return Ok(id);
    }
}
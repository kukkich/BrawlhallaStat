using BrawlhallaStat.Api.BrawlhallaEntities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.BrawlhallaEntities;

[Route("api/[controller]/[action]")]
public class EntitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EntitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Legends()
    {
        var result = await _mediator.Send(new LegendsQuery());

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Weapons()
    {
        var result = await _mediator.Send(new LegendsQuery());

        return Ok(result);
    }
}
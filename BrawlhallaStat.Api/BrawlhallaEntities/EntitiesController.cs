using BrawlhallaStat.Api.BrawlhallaEntities.Queries;
using BrawlhallaStat.Api.BrawlhallaEntities.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.BrawlhallaEntities;

[Authorize]
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
        var result = await _mediator.Send(new WeaponsQuery());

        return Ok(result);
    }

    [HttpPost]
    [ActionName("weapons")]
    [Authorize("Editor")]
    public async Task<IActionResult> AddWeapons([FromBody] AddWeaponRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost]
    [ActionName("legends")]
    [Authorize("Editor")]
    public async Task<IActionResult> AddLegends([FromBody] AddLegendRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}
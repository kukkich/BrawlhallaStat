using AutoMapper;
using BrawlhallaStat.Domain.Statistics.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BrawlhallaStat.Api.Commands.Statistic;
using BrawlhallaStat.Domain.Identity;
using MediatR;

namespace BrawlhallaStat.Api.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class StatisticController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public StatisticController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LegendStatisticDto>>> Legends()
    {
        ClaimsPrincipal userClaims = HttpContext.User;
        var user = _mapper.Map<AuthenticatedUser>(userClaims);
        
        var result = await _mediator.Send(new GetLegendsStatistic(user));

        return Ok(result);
    }
}
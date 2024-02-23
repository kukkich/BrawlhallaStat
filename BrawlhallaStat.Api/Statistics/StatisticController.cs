using AutoMapper;
using BrawlhallaStat.Api.Statistics.Queries;
using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Statistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Statistics;

[Authorize]
[Route("api/[controller]")]
public class StatisticController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StatisticController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> FilteredStatistic(StatisticGeneralFilter filter)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var result = await _mediator.Send(new StatisticQuery(user, filter));

        return Ok(result);
    }
}
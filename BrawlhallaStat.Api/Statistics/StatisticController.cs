using BrawlhallaStat.Api.Statistics.Queries;
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

    public StatisticController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FilteredStatistic(StatisticGeneralFilter filter)
    {
        var user = TestUser.Instance;

        var result = await _mediator.Send(new StatisticQuery(user, filter));

        return Ok(result);
    }
}
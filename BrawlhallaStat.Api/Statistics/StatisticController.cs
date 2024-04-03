using AutoMapper;
using BrawlhallaStat.Api.General.Paging;
using BrawlhallaStat.Api.Statistics.Queries;
using BrawlhallaStat.Api.Statistics.Requests;
using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Statistics;

[Authorize]
[Route("api/[controller]/[action]")]
public class StatisticController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IValidator<Page> _pageValidator;

    public StatisticController(IMediator mediator, IMapper mapper, IValidator<Page> pageValidator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _pageValidator = pageValidator;
    }

    [HttpGet]
    public async Task<ActionResult<Statistic>> CustomFilter(StatisticFilterCreateDto filter)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var result = await _mediator.Send(new StatisticByFilterQuery(user, filter));

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatisticWithFilterDto>>> UserStatistics()
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var result = await _mediator.Send(new StatisticsQuery(user));

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedStatisticWithFilterDto>> UserStatisticsPaged(
        [FromQuery] Page page
    )
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var validationResult = _pageValidator.Validate(page);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var result = await _mediator.Send(new StatisticsPagedQuery(user, page));

        return Ok(result);
    }

    [HttpPost]
    [ActionName("filters")]
    public async Task<ActionResult<StatisticWithFilterDto>> AddFilter([FromBody] StatisticFilterCreateDto? filter)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);
        if (filter is null || !filter.IsValid())
        {
            string[] errors = ["Invalid filter"];
            return BadRequest(errors);
        }

        var result = await _mediator.Send(new AddFilterRequest(user, filter));

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ActionName("filters")]
    public async Task<ActionResult> DeleteFilter(string id)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        await _mediator.Send(new DeleteFilterRequest(user, id));

        return Ok();
    }
}
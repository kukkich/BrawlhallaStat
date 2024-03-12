﻿using AutoMapper;
using BrawlhallaStat.Api.Statistics.Queries;
using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
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

    public StatisticController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Statistic>> CustomFilter(StatisticFilterCreateDto filterCreate)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var result = await _mediator.Send(new StatisticQuery(user, filterCreate));

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatisticWithFilter>>> UserFilters()
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        throw new NotImplementedException();
        
        //return Ok(result);
    }

    [HttpPost("filters")]
    public async Task<ActionResult<StatisticWithFilter>> AddFilter([FromBody] StatisticFilterCreateDto filterCreate)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        throw new NotImplementedException();

        //return Ok(result);
    }

    [HttpDelete("filters")]
    public async Task<ActionResult> DeleteFilter([FromBody] string id)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        throw new NotImplementedException();

        //return Ok(result);
    }
}
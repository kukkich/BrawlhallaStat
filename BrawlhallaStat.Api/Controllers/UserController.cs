using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers;

[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMoq([FromBody] RegisterUser request)
    {
        try
        {
            var id = await _mediator.Send(request);
            return Ok(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, String.Empty);
            throw;
        }
    }
}
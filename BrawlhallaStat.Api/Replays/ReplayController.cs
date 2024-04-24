using AutoMapper;
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using BrawlhallaStat.Api.Replays.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Replays;

[Authorize]
[Route("api/[controller]/[action]")]
public class ReplayController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ReplayController> _logger;

    public ReplayController(IMediator mediator, IMapper mapper, ILogger<ReplayController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file is null || file.Length <= 0)
        {
            _logger.LogDebug("Invalid file received");
            return BadRequest("Invalid file");
        }

        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        await _mediator.Send(new UploadReplayRequest(user, file));

        return Ok("Replay uploaded");
    }
}

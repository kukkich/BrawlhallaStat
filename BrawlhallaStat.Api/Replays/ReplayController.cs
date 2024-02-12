using AutoMapper;
using BrawlhallaStat.Api.Replays.Requests;
using BrawlhallaStat.Domain.Identity;
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

    public ReplayController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file is null || file.Length <= 0)
        {
            return BadRequest("Invalid file");
        }

        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        await _mediator.Send(new UploadReplayRequest(user, file));

        return Ok("Replay uploaded");
    }
}

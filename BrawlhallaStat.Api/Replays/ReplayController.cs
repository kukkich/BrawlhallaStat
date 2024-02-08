using BrawlhallaStat.Api.Replays.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Replays;

[Authorize]
[Route("api/[controller]/[action]")]
public class ReplayController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReplayController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file is null || file.Length <= 0)
        {
            return BadRequest("Invalid file");
        }

        var user = TestUser.Instance;

        await _mediator.Send(new UploadReplayCommand(user, file));

        return Ok("Replay uploaded");
    }
}

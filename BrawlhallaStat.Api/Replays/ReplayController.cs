using BrawlhallaStat.Api.Replays.Commands;
using BrawlhallaStat.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Replays;

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
            return BadRequest("Incorrect file");
        }

        var user = new User
        {
            Id = "8857f722-6a29-4c87-9ef2-42ab8c6fa2e5",
            Login = "Nasral V Szhopu"
        };

        await _mediator.Send(new UploadReplayCommand(user, file));

        return Ok("Файл успешно загружен");
    }
}

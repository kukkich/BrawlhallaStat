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
            Id = "3a54046f-a17b-4757-87e9-3b3a7847d8e3",
            Login = "Nasral V Szhopu"
        };

        await _mediator.Send(new UploadReplayCommand(user, file));

        return Ok("Файл успешно загружен");
    }
}

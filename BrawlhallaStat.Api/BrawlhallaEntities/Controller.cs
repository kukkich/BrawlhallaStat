using BrawlhallaStat.Api.Replays.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.BrawlhallaData;

[Route("api/[controller]/[action]")]
public class Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public Controller(IMediator mediator)
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

        var user = TestUser.Instance;

        await _mediator.Send();

        return Ok("Файл успешно загружен");
    }
}
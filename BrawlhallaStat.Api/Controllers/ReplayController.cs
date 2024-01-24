using AutoMapper;
using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Replays.Commands;
using BrawlhallaStat.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers;

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
            return BadRequest("Ошибка при загрузке файла");
        }

        var user = new User()
        {
            Id = "8857f722-6a29-4c87-9ef2-42ab8c6fa2e5",
            Login = "Nasral V Szhopu"
        };

        await _mediator.Send(new UploadReplayCommand(user, file));

        return Ok("Файл успешно загружен");
    }
}

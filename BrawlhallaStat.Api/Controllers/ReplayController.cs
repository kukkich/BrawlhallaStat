using Microsoft.AspNetCore.Mvc;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.Controllers;

public class ReplayController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReplayController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public IActionResult Upload(IFormFile? file)
    {
        if (file is null || file.Length <= 0)
        {
            return BadRequest("Ошибка при загрузке файла");
        }
        
        // Вы можете выполнить дополнительную обработку файла здесь, если необходимо
        
        _mediator.

        return Ok("Файл успешно загружен и сохранен в базе данных");
    }
}

using AutoMapper;
using BrawlhallaStat.Api.Commands;
using Microsoft.AspNetCore.Mvc;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Base;
using MediatR;

namespace BrawlhallaStat.Api.Controllers;

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
    public IActionResult Upload(IFormFile? file)
    {
        if (file is null || file.Length <= 0)
        {
            return BadRequest("Ошибка при загрузке файла");
        }
        
        var user = _mapper.Map<IUserIdentity>(HttpContext.User);

        _mediator.Send(new UploadReplay(user, file));

        return Ok("Файл успешно загружен");
    }
}

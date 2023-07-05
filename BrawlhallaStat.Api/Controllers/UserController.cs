using BrawlhallaStat.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers;

[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUser request)
    {
        try
        {
            var id = await _mediator.Send(request);
            return Ok(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}
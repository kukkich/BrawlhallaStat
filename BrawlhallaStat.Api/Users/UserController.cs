using AutoMapper;
using BrawlhallaStat.Api.Users.Requests;
using BrawlhallaStat.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Users;

[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangeNickName([FromBody] string newNickName)
    {
        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var request = new ChangeNickNameRequest(user, newNickName);
        await _mediator.Send(request);

        return Ok();
    } 
}
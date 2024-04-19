using AutoMapper;
using BrawlhallaStat.Api.Users.Requests;
using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Identity.Dto;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Users;

[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateUserProfile> _profileValidator;

    public UserController(
        IMediator mediator, 
        IMapper mapper, 
        IValidator<UpdateUserProfile> profileValidator
        )
    {
        _mediator = mediator;
        _mapper = mapper;
        _profileValidator = profileValidator;
    }

    [HttpPost]
    [Authorize]
    [ActionName("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfile? newProfile)
    {
        if (newProfile is null)
        {
            return BadRequest(new []{$"{nameof(newProfile)} is null"});
        }
        var validationResult = _profileValidator.Validate(newProfile);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var user = _mapper.Map<AuthenticatedUser>(HttpContext.User);

        var request = new UpdateProfileRequest(user, newProfile);
        await _mediator.Send(request);

        return Ok();
    }
}
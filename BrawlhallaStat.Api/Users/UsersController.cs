using AutoMapper;
using BrawlhallaStat.Api.Contracts.Identity;
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using BrawlhallaStat.Api.Users.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Users;

[Route("api/[controller]/{userId:guid}")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateUserProfile> _profileValidator;

    public UsersController(
        IMediator mediator, 
        IMapper mapper, 
        IValidator<UpdateUserProfile> profileValidator
        )
    {
        _mediator = mediator;
        _mapper = mapper;
        _profileValidator = profileValidator;
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromRoute]Guid userId, [FromBody] UpdateUserProfile? newProfile)
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
        //todo extract in action filter
        if (userId.ToString() != user.Id)
        {
            return new ForbidResult();
        }

        var request = new UpdateProfileRequest(user, newProfile);
        await _mediator.Send(request);

        return Ok();
    }
}
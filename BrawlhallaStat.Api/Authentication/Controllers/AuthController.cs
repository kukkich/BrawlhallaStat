using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Authentication.Requests.Logout;
using BrawlhallaStat.Api.Authentication.Requests.Refresh;
using BrawlhallaStat.Api.Authentication.Requests.Register;
using BrawlhallaStat.Domain.Identity.Dto;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Authentication.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private const string RefreshTokenCookieKey = "refreshToken";

    private readonly IMediator _mediator;
    private readonly IValidator<RegistrationModel> _registrationValidator;
    private readonly IValidator<LoginUserRequest> _loginValidator;

    public AuthController(IMediator mediator, 
        IValidator<RegistrationModel> registrationValidator,
        IValidator<LoginUserRequest> loginValidator
    )
    {
        _mediator = mediator;
        _registrationValidator = registrationValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Register([FromBody] RegisterUserRequest request)
    {
        var validationResult = _registrationValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var tokens = await _mediator.Send(request);

        SetTokenInCookie(tokens.Refresh);

        return Ok(tokens.Access);
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Login([FromBody] LoginUserRequest request)
    {
        var validationResult = _loginValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var tokens = await _mediator.Send(request);

        SetTokenInCookie(tokens.Refresh);

        return Ok(tokens.Access);
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Refresh()
    {
        string? refreshToken = HttpContext.Request.Cookies[RefreshTokenCookieKey];
        if (refreshToken is null)
        {
            return BadRequest();
        }

        var command = new RefreshTokenRequest { RefreshToken = refreshToken };

        var tokens = await _mediator.Send(command);
        SetTokenInCookie(tokens.Refresh);

        return Ok(tokens.Access);
    }

    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        string? refreshToken = HttpContext.Request.Cookies[RefreshTokenCookieKey];
        if (refreshToken is null)
        {
            return BadRequest();
        }

        var command = new LogoutUserRequest { RefreshToken = refreshToken };

        await _mediator.Send(command);

        HttpContext.Response.Cookies.Delete(RefreshTokenCookieKey);

        return Ok();
    }

    private void SetTokenInCookie(string token)
    {
        HttpContext.Response.Cookies.Append(
            key: RefreshTokenCookieKey,
            value: token,
            new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(10), //Todo take from configuration
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            }
        );
    }
}
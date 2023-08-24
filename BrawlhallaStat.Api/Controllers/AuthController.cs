using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private const string RefreshTokenCookieKey = "refreshToken";

    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResult>> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        SetTokenInCookie(result.TokenPair);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginUserCommand command)
    {
        var loginResult = await _mediator.Send(command);

        SetTokenInCookie(loginResult.TokenPair);

        return Ok(loginResult);
    }

    [HttpPost]
    public async Task<ActionResult<LoginResult>> Refresh()
    {
        string? refreshToken = HttpContext.Request.Cookies[RefreshTokenCookieKey];
        if (refreshToken is null)
        {
            return BadRequest();
        }

        var command = new RefreshTokenCommand { RefreshToken = refreshToken };

        var loginResult = await _mediator.Send(command);
        SetTokenInCookie(loginResult.TokenPair);

        return Ok(loginResult);
    }

    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        string? refreshToken = HttpContext.Request.Cookies[RefreshTokenCookieKey];
        if (refreshToken is null)
        {
            return BadRequest();
        }

        var command = new LogoutUserCommand { RefreshToken = refreshToken };

        await _mediator.Send(command);

        HttpContext.Response.Cookies.Delete(RefreshTokenCookieKey);

        return Ok();
    }

    private void SetTokenInCookie(TokenPair tokens)
    {
        HttpContext.Response.Cookies.Append(
            key: RefreshTokenCookieKey,
            value: tokens.Refresh,
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
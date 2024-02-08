using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Authentication.Requests.Logout;
using BrawlhallaStat.Api.Authentication.Requests.Refresh;
using BrawlhallaStat.Api.Authentication.Requests.Register;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Authentication.Controllers;

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
    public async Task<ActionResult<TokenPair>> Register([FromBody] RegisterUserRequest command)
    {
        var tokens = await _mediator.Send(command);

        SetTokenInCookie(tokens.Refresh);

        return Ok(tokens.Access);
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Login([FromBody] LoginUserRequest command)
    {
        var tokens = await _mediator.Send(command);

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
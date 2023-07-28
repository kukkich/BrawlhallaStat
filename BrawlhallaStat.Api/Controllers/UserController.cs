using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers;

[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private const string RefreshTokenCookieKey = "refreshToken";

    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMoq([FromBody] RegisterUser request)
    {
        try
        {
            var id = await _mediator.Send(request);
            return Ok(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, String.Empty);
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Register([FromBody] RegisterUserCommand command)
    {
        var tokens = await _mediator.Send(command);

        SetTokenInCookie(tokens);

        return Ok(tokens);
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Login([FromBody] LoginUserCommand command)
    {
        var tokens = await _mediator.Send(command);

        SetTokenInCookie(tokens);

        return Ok(tokens);
    }

    [HttpPost]
    public async Task<ActionResult<TokenPair>> Refresh([FromBody] RefreshTokenCommand command)
    {
        var tokens = await _mediator.Send(command);
        SetTokenInCookie(tokens);

        return Ok(tokens);
    }

    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutUserCommand command)
    {
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
                MaxAge = TimeSpan.FromDays(30), //Todo take from configuration
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            }
        );
    }

}
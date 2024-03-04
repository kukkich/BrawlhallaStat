using AutoMapper;
using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Authentication.Requests.Logout;
using BrawlhallaStat.Api.Authentication.Requests.Refresh;
using BrawlhallaStat.Api.Authentication.Requests.Register;
using BrawlhallaStat.Domain.Identity.Authentication.Dto;
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
    private readonly IValidator<LoginModel> _loginValidator;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthController(
        IMediator mediator, 
        IValidator<RegistrationModel> registrationValidator,
        IValidator<LoginModel> loginValidator,
        IConfiguration configuration,
        IMapper mapper
    )
    {
        _mediator = mediator;
        _registrationValidator = registrationValidator;
        _loginValidator = loginValidator;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResultDto>> Register([FromBody] RegisterUserRequest request)
    {
        var validationResult = _registrationValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var result = await _mediator.Send(request);

        SetTokenInCookie(result.TokenPair.Refresh);

        var resultDto = _mapper.Map<LoginResultDto>(result);
        return Ok(resultDto);
    }

    [HttpPost]
    public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginUserRequest request)
    {
        var validationResult = _loginValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var result = await _mediator.Send(request);

        SetTokenInCookie(result.TokenPair.Refresh);

        var resultDto = _mapper.Map<LoginResultDto>(result);
        return Ok(resultDto);
    }

    [HttpPost]
    public async Task<ActionResult<LoginResultDto>> Refresh()
    {
        string? refreshToken = HttpContext.Request.Cookies[RefreshTokenCookieKey];
        if (refreshToken is null)
        {
            return BadRequest();
        }

        var command = new RefreshTokenRequest { RefreshToken = refreshToken };

        var result = await _mediator.Send(command);

        SetTokenInCookie(result.TokenPair.Refresh);

        var resultDto = _mapper.Map<LoginResultDto>(result);
        return Ok(resultDto);
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
                MaxAge = TimeSpan.FromDays(_configuration.GetSection("Auth").GetValue<int>("CookieLifetimeDays")),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            }
        );
    }
}
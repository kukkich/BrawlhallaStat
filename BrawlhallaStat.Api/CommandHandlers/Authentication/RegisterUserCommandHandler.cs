using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using System.Security.Claims;
using AutoMapper;
using BrawlhallaStat.Api.Services.Tokens;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenPair>
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RegisterUserCommandHandler(
        ITokenService tokenService, 
        IMediator mediator, 
        IMapper mapper,
        ILogger<RefreshTokenCommandHandler> logger
        )
    {
        _tokenService = tokenService;
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TokenPair> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (login, password, email) = request;
        var createUserCommand = new RegisterUser
        {
            Email = email,
            Login = login,
            Password = password,
        };

        var user = await _mediator.Send(createUserCommand, cancellationToken);

        var userClaims = _mapper.Map<List<Claim>>(user);
        var tokenPair = _tokenService.GenerateTokenPair(userClaims);

        await _tokenService.SaveToken(user.Id, tokenPair.Refresh);

        _logger.LogInformation("Register user {role.Login} [{role.Id}]", user.Login, user.Id);
        
        return tokenPair;
    }
}
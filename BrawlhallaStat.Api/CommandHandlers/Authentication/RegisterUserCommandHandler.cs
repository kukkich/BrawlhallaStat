using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using BrawlhallaStat.Api.Services.Tokens;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenPair>
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RegisterUserCommandHandler(
        ITokenService tokenService, 
        IMediator mediator, 
        ILogger<RefreshTokenCommandHandler> logger
        )
    {
        _tokenService = tokenService;
        _mediator = mediator;
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

        var tokenPair = await _tokenService.GenerateTokenPair(user);

        _logger.LogInformation("Register user {role.Login} [{role.Id}]", user.Login, user.Id);
        
        return tokenPair;
    }
}
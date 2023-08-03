using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Services.Tokens;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private readonly ITokenService _tokenService;

    public LogoutUserCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _tokenService.RevokeRefreshToken(request.RefreshToken);
    }
}
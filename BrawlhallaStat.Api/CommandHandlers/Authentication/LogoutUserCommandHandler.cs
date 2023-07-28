using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Services.Token;
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
        // Реализация логики выхода пользователя (отзыв refresh токена)
        // ...

        throw new NotImplementedException();

        _tokenService.RevokeRefreshToken(request.RefreshToken);
    }
}
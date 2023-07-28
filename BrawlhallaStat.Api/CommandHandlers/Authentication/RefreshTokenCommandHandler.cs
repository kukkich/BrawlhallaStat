using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Services.Token;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenPair>
{
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TokenPair> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Реализация логики обновления access токена по refresh токену
        // ...

        // Вернуть новые access и refresh токены

        throw new NotImplementedException();

        return _tokenService.RefreshAccessToken(request.RefreshToken);
    }
}
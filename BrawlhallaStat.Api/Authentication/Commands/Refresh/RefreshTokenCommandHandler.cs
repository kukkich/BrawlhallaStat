using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Services.Tokens;
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
        var tokenPair = await _tokenService.RefreshAccessToken(request.RefreshToken);

        return tokenPair;
    }
}
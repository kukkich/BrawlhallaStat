using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Services.Tokens;
using BrawlhallaStat.Domain.Identity.Dto;
using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenPair>
{
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TokenPair> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Реализация логики входа пользователя
        // ...

        // Вернуть сгенерированные токены
        throw new NotImplementedException();

        return _tokenService.GenerateTokenPair(user);
    }
}
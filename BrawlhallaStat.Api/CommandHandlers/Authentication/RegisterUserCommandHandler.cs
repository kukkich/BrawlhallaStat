using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Services.Token;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenPair>
{
    private readonly ITokenService _tokenService;

    public RegisterUserCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TokenPair> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Реализация логики регистрации пользователя
        // ...

        // Вернуть сгенерированные токены
        throw new NotImplementedException();

        return _tokenService.GenerateTokenPair(user);
    }
}
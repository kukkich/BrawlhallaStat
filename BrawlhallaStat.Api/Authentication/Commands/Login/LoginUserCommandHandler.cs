using BrawlhallaStat.Api.Exceptions.Authentication;
using BrawlhallaStat.Api.Services.Tokens;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Authentication.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenPair>
{
    private readonly ITokenService _tokenService;
    private readonly BrawlhallaStatContext _dbContext;

    public LoginUserCommandHandler(ITokenService tokenService, BrawlhallaStatContext dbContext)
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<TokenPair> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(
                x => x.Login == request.Login,
                cancellationToken: cancellationToken
            );
        if (user is null)
        {
            throw new UserNotExistException(request.Login);
        }
        
        //TODO make a password hashing
        if (user.PasswordHash != request.Password)
        {
            throw new InvalidPasswordException();
        }

        var newTokenPair = await _tokenService.GenerateTokenPair(user);

        return newTokenPair;
    }
}


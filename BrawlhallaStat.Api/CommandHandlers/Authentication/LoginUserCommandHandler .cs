using AutoMapper;
using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Exceptions.Authentication;
using BrawlhallaStat.Api.Services.Tokens;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResult>
{
    private readonly ITokenService _tokenService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(
        ITokenService tokenService, 
        BrawlhallaStatContext dbContext,
        IMapper mapper
        )
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
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

        return new LoginResult
        {
            TokenPair = newTokenPair,
            User = _mapper.Map<AuthenticatedUser>(user)
        };
    }
}


﻿using AutoMapper;
using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Commands.Authentication;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using BrawlhallaStat.Api.Services.Tokens;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;
using BrawlhallaStat.Domain.Identity;

namespace BrawlhallaStat.Api.CommandHandlers.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, LoginResult>
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly BrawlhallaStatContext _dbContext;

    public RegisterUserCommandHandler(
        ITokenService tokenService,
        BrawlhallaStatContext dbContext,
        IMediator mediator, 
        ILogger<RefreshTokenCommandHandler> logger,
        IMapper mapper
        )
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<LoginResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (login, password, email) = request;

        if (await _dbContext.Users.AnyAsync(x => x.Login == login, cancellationToken: cancellationToken))
        {
            throw new AlreadyExistException(
                who: nameof(User),
                propertyName: nameof(User.Login),
                value: login
            );
        }
        if (await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellationToken))
        {
            throw new AlreadyExistException(
                who: nameof(User),
                propertyName: nameof(User.Email),
                value: email
            );
        }

        var createUserCommand = new CreateUser
        {
            Email = email,
            Login = login,
            Password = password,
        };

        var user = await _mediator.Send(createUserCommand, cancellationToken);
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var tokenPair = await _tokenService.GenerateTokenPair(user);

        _logger.LogInformation("Register user {role.Login} [{role.Id}]", user.Login, user.Id);

        return new LoginResult
        {
            TokenPair = tokenPair,
            User = _mapper.Map<AuthenticatedUser>(user)
        };
    }
}
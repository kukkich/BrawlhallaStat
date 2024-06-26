﻿using AutoMapper;
using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Register;

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, LoginResult>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;
    private readonly IMapper _mapper;

    public RegisterUserRequestHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserRequest> logger,
        IMapper mapper
        )
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<LoginResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "User {Login} {Email} registration transaction begin",
                request.Login, request.Email
            );

            var registrationData = _mapper.Map<RegistrationData>(request);
            var result = await _authService.Register(registrationData);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "User {Login} {Email} registration transaction commit",
                request.Login, request.Email
            );

            return result;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "User {Login} {Email} registration transaction rollback {Message}",
                request.Login, request.Email, exception.Message
            );
            throw;
        }
    }
}
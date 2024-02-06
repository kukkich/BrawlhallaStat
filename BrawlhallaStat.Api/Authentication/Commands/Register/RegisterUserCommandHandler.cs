using AutoMapper;
using BrawlhallaStat.Api.Authentication.Commands.Login;
using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenPair>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserCommand> _logger;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserCommand> logger,
        IMapper mapper
        )
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<TokenPair> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "User {Login} {Email} registration transaction begin",
                request.Login, request.Email
            );

            var registrationData = _mapper.Map<RegistrationData>(request);
            var tokenPair = await _authService.Register(registrationData);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "User {Login} {Email} registration transaction commit",
                request.Login, request.Email
            );

            return tokenPair;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "User {Login} {Email} registration transaction rollback",
                request.Login, request.Email
            );
            throw;
        }
    }
}
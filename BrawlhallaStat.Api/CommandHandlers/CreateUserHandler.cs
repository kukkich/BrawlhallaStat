using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Factories;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Base;
using MediatR;
namespace BrawlhallaStat.Api.CommandHandlers;

public class CreateUserHandler : IRequestHandler<RegisterUser, IUserIdentity>
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IStatisticFactory _statisticFactory;

    public CreateUserHandler(BrawlhallaStatContext dbContext, IStatisticFactory statisticFactory)
    {
        _dbContext = dbContext;
        _statisticFactory = statisticFactory;
    }

    //TODO move in userFactory
    public async Task<IUserIdentity> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        //TODO ensure there's no user with same email/login
        var userId = Guid.NewGuid().ToString();
        
        var user = new User
        {
            Id = userId,
            Login = request.Login,
            NickName = request.Login,
            Email = request.Email,
            PasswordHash = request.Password,

            TotalStatistic = _statisticFactory.CreateSimple(),
            WeaponStatistics = await _statisticFactory.CreateWeapon(userId),
            LegendStatistics = await _statisticFactory.CreateLegend(userId),
            LegendAgainstLegendStatistics = await _statisticFactory.CreateLegendAgainstLegend(userId),
            LegendAgainstWeaponStatistics = await _statisticFactory.CreateLegendAgainstWeapon(userId),
            WeaponAgainstWeaponStatistics = await _statisticFactory.CreateWeaponAgainstWeapon(userId),
            WeaponAgainstLegendStatistics = await _statisticFactory.CreateWeaponAgainstLegend(userId),

            Roles = new(),
            Claims = new()
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }
}
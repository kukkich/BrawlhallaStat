using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Factories;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;
namespace BrawlhallaStat.Api.CommandHandlers;

public class RegisterUserHandler : IRequestHandler<RegisterUser, string>
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IStatisticFactory _statisticFactory;

    public RegisterUserHandler(BrawlhallaStatContext dbContext, IStatisticFactory statisticFactory)
    {
        _dbContext = dbContext;
        _statisticFactory = statisticFactory;
    }

    public async Task<string> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid().ToString();
        var user = new User
        {
            Id = userId,
            Login = request.Login,
            TelegramId = request.TelegramId,
            TotalStatistic = _statisticFactory.CreateSimple(),
            WeaponStatistics = await _statisticFactory.CreateWeapon(userId),
            LegendStatistics = await _statisticFactory.CreateLegend(userId),
            LegendAgainstLegendStatistics = await _statisticFactory.CreateLegendAgainstLegend(userId),
            LegendAgainstWeaponStatistics = await _statisticFactory.CreateLegendAgainstWeapon(userId),
            WeaponAgainstWeaponStatistics = await _statisticFactory.CreateWeaponAgainstWeapon(userId),
            WeaponAgainstLegendStatistics = await _statisticFactory.CreateWeaponAgainstLegend(userId),
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return userId;
    }
}
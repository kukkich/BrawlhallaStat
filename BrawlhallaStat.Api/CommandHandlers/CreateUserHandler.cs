using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Factories;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;
namespace BrawlhallaStat.Api.CommandHandlers;

public class CreateUserHandler : IRequestHandler<CreateUser, User>
{
    private readonly IStatisticFactory _statisticFactory;

    public CreateUserHandler(IStatisticFactory statisticFactory)
    {
        _statisticFactory = statisticFactory;
    }

    //TODO move in userFactory
    public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
    {
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

        return user;
    }
}
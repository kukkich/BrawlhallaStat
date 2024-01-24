using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Factories;
using BrawlhallaStat.Domain;
using MediatR;
namespace BrawlhallaStat.Api.CommandHandlers;

public class CreateUserHandler : IRequestHandler<CreateUser, User>
{
    private readonly IStatisticFactory _statisticFactory;

    public CreateUserHandler(IStatisticFactory statisticFactory)
    {
        _statisticFactory = statisticFactory;
    }

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

            Roles = new(),
            Claims = new()
        };

        return user;
    }
}
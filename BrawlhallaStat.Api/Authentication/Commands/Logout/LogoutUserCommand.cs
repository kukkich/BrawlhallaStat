using MediatR;

namespace BrawlhallaStat.Api.Authentication.Commands.Logout;

public class LogoutUserCommand : IRequest
{
    public string RefreshToken { get; set; } = null!;
}
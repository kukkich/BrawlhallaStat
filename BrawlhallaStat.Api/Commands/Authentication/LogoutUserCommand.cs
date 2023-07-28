using MediatR;

namespace BrawlhallaStat.Api.Commands.Authentication;

public class LogoutUserCommand : IRequest
{
    public string RefreshToken { get; set; } = null!;
}
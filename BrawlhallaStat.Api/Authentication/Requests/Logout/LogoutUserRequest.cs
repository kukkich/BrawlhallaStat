using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Logout;

public class LogoutUserRequest : IRequest
{
    public string RefreshToken { get; set; } = null!;
}
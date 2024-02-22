using BrawlhallaStat.Domain.Identity.Authentication;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Refresh;

public class RefreshTokenRequest : IRequest<LoginResult>
{
    public string RefreshToken { get; set; } = null!;
}
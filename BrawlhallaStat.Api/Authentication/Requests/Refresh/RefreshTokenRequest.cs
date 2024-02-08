using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Refresh;

public class RefreshTokenRequest : IRequest<TokenPair>
{
    public string RefreshToken { get; set; } = null!;
}
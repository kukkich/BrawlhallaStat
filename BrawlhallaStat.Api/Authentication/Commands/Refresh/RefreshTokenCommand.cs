using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Commands.Refresh;

public class RefreshTokenCommand : IRequest<TokenPair>
{
    public string RefreshToken { get; set; } = null!;
}
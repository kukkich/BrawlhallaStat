using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Commands.Authentication;

public class RefreshTokenCommand : IRequest<TokenPair>
{
    public string RefreshToken { get; set; } = null!;
}
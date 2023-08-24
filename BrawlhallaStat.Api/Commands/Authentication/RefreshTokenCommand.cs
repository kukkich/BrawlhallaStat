using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Commands.Authentication;

public class RefreshTokenCommand : IRequest<LoginResult>
{
    public string RefreshToken { get; set; } = null!;
}
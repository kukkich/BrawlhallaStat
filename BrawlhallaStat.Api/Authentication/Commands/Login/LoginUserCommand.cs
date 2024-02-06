using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Commands.Login;

public class LoginUserCommand : IRequest<TokenPair>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
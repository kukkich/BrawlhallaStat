using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Commands.Authentication;

public class LoginUserCommand : IRequest<LoginResult>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Login;

public class LoginUserRequest : IRequest<TokenPair>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
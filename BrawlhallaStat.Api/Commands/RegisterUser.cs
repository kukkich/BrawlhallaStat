using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Commands;

public class RegisterUser : IRequest<IUserIdentity>
{
    public string Login { get; set; } = null!;
    public string Password {get; set;} = null!;
    public string Email { get; set; } = null!;
}
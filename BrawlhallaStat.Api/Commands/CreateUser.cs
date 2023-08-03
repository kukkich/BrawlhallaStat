using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.Commands;

public class CreateUser : IRequest<User>
{
    public string Login { get; set; } = null!;
    public string Password {get; set;} = null!;
    public string Email { get; set; } = null!;
}
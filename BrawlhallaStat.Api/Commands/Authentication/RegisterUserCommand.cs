using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Commands.Authentication;

public class RegisterUserCommand : IRequest<LoginResult>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public void Deconstruct(out string login, out string password, out string email)
    {
        login = Login;
        password = Password; 
        email = Email;
    }
}
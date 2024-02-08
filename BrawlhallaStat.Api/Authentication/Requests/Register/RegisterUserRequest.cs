using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Register;

public class RegisterUserRequest : IRequest<TokenPair>
{
    public string Login { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public void Deconstruct(out string login, out string nickName, out string password, out string email)
    {
        login = Login;
        nickName = NickName;
        password = Password;
        email = Email;
    }
}
using BrawlhallaStat.Api.Authentication.Requests.Login;
using FluentValidation;

namespace BrawlhallaStat.Api.Authentication.Validation;

public class LoginRequestValidation : AbstractValidator<LoginUserRequest>
{
    public LoginRequestValidation()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
    }
}
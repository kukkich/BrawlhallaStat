using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using FluentValidation;

namespace BrawlhallaStat.Api.Authentication.Validation;

public class LoginModelValidation : AbstractValidator<LoginModel>
{
    public LoginModelValidation()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
    }
}
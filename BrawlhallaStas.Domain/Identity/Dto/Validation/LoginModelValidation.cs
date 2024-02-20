using BrawlhallaStat.Api.Authentication.Validation;
using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Dto.Validation;

public class LoginModelValidation : AbstractValidator<LoginModel>
{
    public LoginModelValidation()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
    }
}
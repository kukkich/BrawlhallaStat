using BrawlhallaStat.Api.Authentication.Requests.Register;
using FluentValidation;

namespace BrawlhallaStat.Api.Authentication.Validation;

public class RegisterRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        RuleFor(x => x.NickName).NotEmpty().Length(1, 30)
            .WithMessage("NickName must contain from 1 to 30 characters");
        RuleFor(x => x.Email).SetValidator(new EmailValidator());
    }
}
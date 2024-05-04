using FluentValidation;

namespace BrawlhallaStat.Api.Authentication.Validation;

public class LoginValidator : AbstractValidator<string>
{
    public LoginValidator()
    {
        RuleFor(login => login).NotEmpty().Length(6, 30)
            .WithMessage("Login must contain from 6 to 30 characters");
    }
}
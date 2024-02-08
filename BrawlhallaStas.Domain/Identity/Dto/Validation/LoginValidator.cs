using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Dto.Validation;

public class LoginValidator : AbstractValidator<string>
{
    public LoginValidator()
    {
        RuleFor(login => login).NotEmpty().Length(6, 30)
            .WithMessage("Login must contain from 6 to 30 characters");
    }
}
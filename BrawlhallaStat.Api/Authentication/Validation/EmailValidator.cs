using FluentValidation;

namespace BrawlhallaStat.Api.Authentication.Validation;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x).EmailAddress()
            .WithMessage("Invalid email address");
    }
}
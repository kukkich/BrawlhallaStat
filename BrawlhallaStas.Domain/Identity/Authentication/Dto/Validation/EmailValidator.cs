using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x).EmailAddress()
            .WithMessage("Invalid email address");
    }
}
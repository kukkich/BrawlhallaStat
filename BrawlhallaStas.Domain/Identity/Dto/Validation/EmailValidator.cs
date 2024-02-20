using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Dto.Validation;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x).EmailAddress()
            .WithMessage("Invalid email address");
    }
}
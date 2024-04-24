using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;

public class NickNameValidator : AbstractValidator<string>
{
    public NickNameValidator()
    {
        RuleFor(x => x).NotEmpty().Length(1, 30)
            .WithMessage("NickName must contain from 1 to 30 characters");
    }
}
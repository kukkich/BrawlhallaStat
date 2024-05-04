using System.Text.RegularExpressions;
using FluentValidation;

namespace BrawlhallaStat.Api.Authentication.Validation;

public partial class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().Length(8, 30)
            .WithMessage("Password must contain from 8 to 30 characters");

        RuleFor(password => password)
            .Matches(FourDigitsAndFourNotDigitsRegex())
            .WithMessage("Password must contain at least 4 digits and at least 4 non-digits");
    }

    [GeneratedRegex(@"^(?=(?:.*?\d){4})(?=(?:.*?\D){4}).*$")]
    private static partial Regex FourDigitsAndFourNotDigitsRegex();
}
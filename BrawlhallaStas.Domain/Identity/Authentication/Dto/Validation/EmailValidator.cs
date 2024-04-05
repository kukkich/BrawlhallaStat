using FluentValidation;
using System.Text.RegularExpressions;

namespace BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;

public partial class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x).Matches(EmailRegex())
            .WithMessage("Invalid email address");
    }
    
    [GeneratedRegex(@"^[\w\.-]+@\w+\.\w{2,4}$")]
    private static partial Regex EmailRegex();
}
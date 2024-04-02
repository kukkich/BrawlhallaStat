using FluentValidation;

namespace BrawlhallaStat.Api.General.Paging;

public class PageValidator : AbstractValidator<Page>
{
    public PageValidator()
    {
        RuleFor(x => x.Size)
            .GreaterThan(0)
            .LessThanOrEqualTo(50)
            .WithMessage("Page size must be between 1 and 50");
        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("Page number must be positive");
    }
}
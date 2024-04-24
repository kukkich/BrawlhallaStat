using BrawlhallaStat.Api.Contracts.Identity;
using BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;
using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Dto.Validation;

public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfile>
{
    public UpdateUserProfileValidator()
    {
        RuleFor(x => x.NickName).SetValidator(new NickNameValidator());
        RuleFor(x => x.Email).SetValidator(new EmailValidator());
    }
}
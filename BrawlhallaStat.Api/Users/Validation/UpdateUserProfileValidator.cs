using BrawlhallaStat.Api.Authentication.Validation;
using BrawlhallaStat.Api.Contracts.Identity;
using FluentValidation;

namespace BrawlhallaStat.Api.Users.Validation;

public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfile>
{
    public UpdateUserProfileValidator()
    {
        RuleFor(x => x.NickName).SetValidator(new NickNameValidator());
        RuleFor(x => x.Email).SetValidator(new EmailValidator());
    }
}
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;

public class RegisterModelValidator : AbstractValidator<RegistrationModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        RuleFor(x => x.NickName).SetValidator(new NickNameValidator());
        RuleFor(x => x.Email).SetValidator(new EmailValidator());
    }
}
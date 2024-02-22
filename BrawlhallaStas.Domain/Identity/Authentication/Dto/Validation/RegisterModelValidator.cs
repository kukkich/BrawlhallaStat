using BrawlhallaStat.Api.Authentication.Validation;
using FluentValidation;

namespace BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;

public class RegisterModelValidator : AbstractValidator<RegistrationModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        RuleFor(x => x.NickName).NotEmpty().Length(1, 30)
            .WithMessage("NickName must contain from 1 to 30 characters");
        RuleFor(x => x.Email).SetValidator(new EmailValidator());
    }
}
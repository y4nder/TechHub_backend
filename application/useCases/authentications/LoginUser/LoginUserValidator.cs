using FluentValidation;

namespace application.useCases.authentications.LoginUser;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required");
    }
}
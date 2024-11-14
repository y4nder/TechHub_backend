using FluentValidation;

namespace application.useCases.authentications.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(4).MaximumLength(10).WithMessage("Username must have between 4 and 10 characters");
        
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("email is required")
            .EmailAddress().WithMessage("Invalid email address");
        
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required");        
    }
}
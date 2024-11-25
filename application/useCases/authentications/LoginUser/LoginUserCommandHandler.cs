using application.Exceptions.AuthenticationExceptions;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.jwt;
using infrastructure.services.passwordService;
using MediatR;

namespace application.useCases.authentications.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IValidator<LoginUserCommand> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IJwtTokenProvider _jwtTokenProvider;

    public LoginUserCommandHandler(IValidator<LoginUserCommand> validator, 
        IUserRepository userRepository, IPasswordHasherService passwordHasherService,
        IJwtTokenProvider jwtTokenProvider)
    {
        _validator = validator;
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
        _jwtTokenProvider = jwtTokenProvider;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw AuthenticationException.ValidationFailed(validationResult.Errors);
        
        var user = await _userRepository.GetUserByEmail(request.Email) ??
                   throw AuthenticationException.InvalidCredentials();

        if (!_passwordHasherService.VerifyHashedPassword(user.Password!, request.Password))
            throw AuthenticationException.InvalidCredentials();
        
        var token = _jwtTokenProvider.GenerateJwtToken(user);

        return new LoginUserResponse
        {
            Message = "User logged in",
            Token = token
        };
        
    }
}
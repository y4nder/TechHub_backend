using application.Events.UserEvents;
using application.Exceptions.AuthenticationExceptions;
using domain.entities;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.passwordService;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.authentications.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IValidator<RegisterUserCommand> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public RegisterUserCommandHandler(IValidator<RegisterUserCommand> validator, IUserRepository userRepository,
        IPasswordHasherService passwordHasherService, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _validator = validator;
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw AuthenticationException.ValidationFailed(validationResult.Errors);
        }
        
        await EnsureUserNameAndEmailUnique(request.Username, request.Email);
        
        var hashedPassword = _passwordHasherService.HashPassword(request.Password);
        var createdUser = User.Create(request.Username, request.Email, hashedPassword, null);
        
        _userRepository.AddUser(createdUser);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        // publish notification
        await _mediator.Publish(new UserCreatedEvent(createdUser), cancellationToken);

        return new RegisterUserResponse
        {
            Message = "User created",
        };
    }

    private async Task EnsureUserNameAndEmailUnique(string username, string email)
    {
        
        if (await _userRepository.CheckUserNameExists(username))
        {
            throw AuthenticationException.UsernameExists(username);
        }

        if (await _userRepository.CheckUserEmailExists(email))
        {
            throw AuthenticationException.EmailExists(email);
        }
        
        
        
    }
}
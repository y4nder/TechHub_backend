using System.Net;
using application.Shared;
using FluentValidation.Results;

namespace application.Exceptions.AuthenticationExceptions;

public class AuthenticationException : HttpRequestException
{
    private AuthenticationException(string? message, Exception? inner, HttpStatusCode? statusCode) : base(message, inner, statusCode)
    {
    }

    private AuthenticationException()
    {
    }

    private AuthenticationException(string? message) : base(message)
    {
    }

    private AuthenticationException(string? message, Exception? inner) : base(message, inner)
    {
    }
    

    public static AuthenticationException UsernameExists(string username)
    {
        return new AuthenticationException($"Username '{username}' already exists", null, HttpStatusCode.Conflict);
    }

    public static AuthenticationException EmailExists(string email)
    {
        return new AuthenticationException($"Email '{email}' already exists", null, HttpStatusCode.Conflict);
    }

    public static AuthenticationException ValidationFailed(List<ValidationFailure> failures)
    {
        var errorMessages = ErrorUtility.TransformErrors(failures);
        return new AuthenticationException(errorMessages, null, HttpStatusCode.BadRequest);
    }

    public static AuthenticationException InvalidCredentials()
    {
        return new AuthenticationException("Invalid credentials", new KeyNotFoundException(), HttpStatusCode.Unauthorized);
    }
    
    
}
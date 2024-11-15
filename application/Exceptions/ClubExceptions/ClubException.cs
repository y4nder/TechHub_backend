using System.Net;
using application.Shared;
using FluentValidation.Results;

namespace application.Exceptions.ClubExceptions;

public class ClubException : HttpRequestException
{
    private ClubException(string? message, Exception? inner, HttpStatusCode? statusCode) : base(message, inner, statusCode)
    {
    }

    public static ClubException ClubValidationError(List<ValidationFailure> failures)
    {
        string errors = ErrorUtility.TransformErrors(failures);
        return new ClubException(errors, null, HttpStatusCode.BadRequest);
    }

    public static Exception CreatorIdNotFound(int creatorId)
    {
        return new ClubException($"The creator id {creatorId} does not exist.", null, HttpStatusCode.NotFound);
    }

    public static Exception ClubCategoryIdNotFound(int clubCategoryId)
    {
        return new ClubException($"The club category id {clubCategoryId} does not exist.", null, HttpStatusCode.NotFound);
    }

    public static Exception ClubNameAlreadyExists(string clubName)
    {
        return new ClubException("The club name already exists.", null, HttpStatusCode.Conflict);
    }
}
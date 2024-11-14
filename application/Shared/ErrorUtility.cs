using FluentValidation.Results;

namespace application.Shared;

public static class ErrorUtility
{
    public static string TransformErrors(List<ValidationFailure> errors)
    {
        var errorMessages = errors.Aggregate("", (current, error) => current + error.ErrorMessage + " || ");
        return errorMessages;
    }
}
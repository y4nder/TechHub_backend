using System.Net;
using application.Exceptions.AuthenticationExceptions;
using Microsoft.AspNetCore.Mvc;

namespace web_api.utils;

public static class ErrorFactory
{
    public static IActionResult CreateErrorResponse(Exception ex)
    {
        // Handle AuthenticationException
        if (ex is AuthenticationException authEx)
        {
            return authEx.StatusCode switch
            {
                HttpStatusCode.Conflict => new ObjectResult(new { message = authEx.Message }) { StatusCode = 409 },
                HttpStatusCode.BadRequest => new ObjectResult(new { message = authEx.Message }) { StatusCode = 400 },
                HttpStatusCode.Unauthorized => new ObjectResult(new { message = authEx.Message }) { StatusCode = 401 },
                HttpStatusCode.Forbidden => new ObjectResult(new { message = authEx.Message }) { StatusCode = 403 },
                _ => new ObjectResult(new { message = "An unexpected authentication error occurred.", detail = authEx.Message }) { StatusCode = 500 },
            };
        }

        // Handle generic HttpRequestException
        if (ex is HttpRequestException httpEx)
        {
            return new ObjectResult(new { message = httpEx.Message }) { StatusCode = 400 };
        }

        // Handle any other unexpected exception
        return new ObjectResult(new { message = "An unexpected error occurred.", detail = ex.Message }) { StatusCode = 500 };
    }
}
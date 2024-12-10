using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace application.utilities.UserContext;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }

    public int GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if ((user is null) || (!user.Identity?.IsAuthenticated ?? true))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }

        return Convert.ToInt32(userId);
    }
}

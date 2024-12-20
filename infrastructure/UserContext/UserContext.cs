using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace infrastructure.UserContext;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }

    public int GetUserId()
    {
        return Convert.ToInt32(IdExtractor());
    }

    public string GetUserIdAsString()
    {
        return IdExtractor()!;
    }

    private string? IdExtractor()
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
        
        return userId;
    }
}

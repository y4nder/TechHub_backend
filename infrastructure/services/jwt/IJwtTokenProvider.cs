using domain.entities;

namespace infrastructure.services.jwt;

public interface IJwtTokenProvider
{
    string GenerateJwtToken(User user);
}
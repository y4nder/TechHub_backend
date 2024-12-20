using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using domain.entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace infrastructure.services.jwt;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtTokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = configuration["Jwt:Key"]
               ?? throw new ArgumentNullException(nameof(configuration), "JWT:Key cannot be null");

        _issuer = configuration["Jwt:Issuer"]
                  ?? throw new ArgumentNullException(nameof(configuration), "JWT:Issuer cannot be null");

        _audience = configuration["Jwt:Audience"]
                    ?? throw new ArgumentNullException(nameof(configuration), "JWT:Audience cannot be null");
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username!),
        };

        SigningCredentials credentials = GetSigningCredentials();

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(7), 
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private SigningCredentials GetSigningCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return credentials;
    }
}
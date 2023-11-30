using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Utils;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateJwtToken(string username, string role, bool isSessionOnly)
    {
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var validityHours = _configuration["JwtSettings:ValidityHours"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
            // Add more claims as needed
        };
        var identity = new ClaimsIdentity(claims, "Bearer");

        DateTime expireDate = DateTime.UtcNow.AddHours(Convert.ToDouble(validityHours));
        if (isSessionOnly) expireDate = DateTime.UtcNow.AddYears(1);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: identity.Claims,
            expires: expireDate,
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
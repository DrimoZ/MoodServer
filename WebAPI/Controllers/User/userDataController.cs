using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Utils;
using Application.UseCases.Users.UserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers.User;

[ApiController]
[Route("api/v1/user")]
public class UserDataController: ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _configuration;

    private readonly UseCaseFetchUserProfile _useCaseFetchUserProfile;

    public UserDataController(TokenService tokenService, ILogger<UserController> logger, IConfiguration configuration, UseCaseFetchUserProfile useCaseFetchUserProfile)
    {
        _tokenService = tokenService;
        _logger = logger;
        _configuration = configuration;
        
        _useCaseFetchUserProfile = useCaseFetchUserProfile;
    }
    
    [HttpGet("userProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult GetUserProfileData()
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserProfile.Execute(data.Token));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    private (string Token, int Role) GetAuthCookieData()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var key = Encoding.ASCII.GetBytes(secretKey!);
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!];
        
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["JwtSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);
        
        var jwtToken = (JwtSecurityToken)validatedToken;
        
        var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        var role = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value);
        
        return (userId, role);
    }
}
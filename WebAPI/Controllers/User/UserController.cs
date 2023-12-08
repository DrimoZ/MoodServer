using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.Services.Utils;
using Application.UseCases.Users.UserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers.User;

[ApiController]
[Route("api/v1/user")]
public class UserController: ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IConfiguration _configuration;

    private readonly UseCaseFetchUserAccount _useCaseFetchUserAccount;
    private readonly UseCaseFetchUserPublications _useCaseFetchUserPublications;
    private readonly UseCaseFetchUserFriends _useCaseFetchUserFriends;
    private readonly UseCaseUpdateUserData _useCaseUpdateUserData;
    private readonly UseCaseGetUserInfoByLogin _useCaseGetUserInfoByLogin;
    private readonly UseCaseGetAllUsers _useCaseGetAllUsers;

   public UserController(ILogger<AuthenticationController> logger, IConfiguration configuration, UseCaseFetchUserAccount useCaseFetchUserAccount, UseCaseFetchUserPublications useCaseFetchUserPublications, UseCaseFetchUserFriends useCaseFetchUserFriends, UseCaseUpdateUserData useCaseUpdateUserData, UseCaseGetAllUsers useCaseGetAllUsers, UseCaseGetUserInfoByLogin useCaseGetUserInfoByLogin)
    {
        _logger = logger;
        _configuration = configuration;
        
        _useCaseFetchUserAccount = useCaseFetchUserAccount;
        _useCaseFetchUserPublications = useCaseFetchUserPublications;
        _useCaseFetchUserFriends = useCaseFetchUserFriends;
        _useCaseUpdateUserData = useCaseUpdateUserData;
        _useCaseGetAllUsers = useCaseGetAllUsers;
        _useCaseGetUserInfoByLogin = useCaseGetUserInfoByLogin;
    }
    
    [HttpGet("profile/account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult GetUserAccountData()
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserAccount.Execute(data.UserId));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }
    
    [HttpGet("profile/publications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult GetUserPublicationsData()
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserPublications.Execute(data.UserId));
        }
        catch (Exception e)
        {
            return Unauthorized(e);
        }
    }
    
    [HttpGet("profile/friends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult GetUserFriendsData()
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserFriends.Execute(data.UserId));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }
    
    [HttpGet("getUsers")]
    public ActionResult<List<DtoOutputUser>> GetAll()
    {
        var data =  GetAuthCookieData();
        return Ok(_useCaseGetAllUsers.Execute(data.UserId));
    }
    
    [HttpPut]
    [HttpGet("otherUsers/{login}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult GetOtherUserData(string login)
    {
        try
        {
            var data = GetAuthCookieData();
            return Ok(_useCaseGetUserInfoByLogin.Execute(data.UserId, login));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Unauthorized();
        }
    }
    
    [HttpPut("userUpdateAccount")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Update(DtoInputUpdateUser dto)
    {
        if (_useCaseUpdateUserData.Execute(dto))
        {
            return NoContent();
        }

        return NotFound();
    }
    private (string UserId, int Role) GetAuthCookieData()
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
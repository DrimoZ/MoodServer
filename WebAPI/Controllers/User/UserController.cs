using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.UseCases.Publications;
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
    private readonly TokenService _tokenService;

    private readonly UseCaseFetchUserAccountByUserId _useCaseFetchUserAccountByUserId;
    private readonly UseCaseFetchUserPublicationByUser _useCaseFetchUserPublicationByUser;
    private readonly UseCaseFetchUserFriendsByUserId _useCaseFetchUserFriendsByUserId;
    private readonly UseCaseUpdateUserData _useCaseUpdateUserData;
    private readonly UseCaseGetUserInfoByLogin _useCaseGetUserInfoByLogin;
    private readonly UseCaseGetAllUsers _useCaseGetAllUsers;
    private readonly UseCaseFetchUserProfileByUserId _useCaseFetchUserProfileByUserId;

    public UserController(ILogger<AuthenticationController> logger, TokenService tokenService, IConfiguration configuration, UseCaseFetchUserAccountByUserId useCaseFetchUserAccountByUserId, UseCaseFetchUserFriendsByUserId useCaseFetchUserFriendsByUserId, UseCaseUpdateUserData useCaseUpdateUserData, UseCaseGetAllUsers useCaseGetAllUsers, UseCaseGetUserInfoByLogin useCaseGetUserInfoByLogin, UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, UseCaseFetchUserProfileByUserId useCaseFetchUserProfileByUserId)
    {
        _logger = logger;
        _configuration = configuration;
        
        _useCaseFetchUserAccountByUserId = useCaseFetchUserAccountByUserId;
        _useCaseFetchUserFriendsByUserId = useCaseFetchUserFriendsByUserId;
        _useCaseUpdateUserData = useCaseUpdateUserData;
        _useCaseGetAllUsers = useCaseGetAllUsers;
        _useCaseGetUserInfoByLogin = useCaseGetUserInfoByLogin;
        _tokenService = tokenService;
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _useCaseFetchUserProfileByUserId = useCaseFetchUserProfileByUserId;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    //Get the connected User Id and his Role 
    public IActionResult GetUserIdAndRole()
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(new {userId = data.UserId, userRole = data.Role});
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }
    
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    //Get the public profile information of given userId
    public IActionResult GetUserProfileByUserId(string userId)
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserProfileByUserId.Execute(data.UserId, userId));
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
   
   
    [HttpGet("{userId}/account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    //Get the public profile account information of given userId
    public IActionResult GetUserAccountByUserId(string userId)
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserAccountByUserId.Execute(data.UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpGet("{userId}/friends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    //Get the friends information of given userId
    public IActionResult GetUserFriendsByUserId(string userId)
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserFriendsByUserId.Execute(data.UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpGet("{userId}/publications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    //Get the publications information of given userId
    public IActionResult GetUserPublicationsData(string userId)
    {
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseFetchUserPublicationByUser.Execute(data.UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    [HttpPut("profile/account")]
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
    
    
    private (string UserId, int Role) GetAuthCookieData()
    {
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        return _tokenService.GetAuthCookieData(token);
    }
}
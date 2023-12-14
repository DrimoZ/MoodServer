using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.Services.Utils;
using Application.UseCases.Publications;
using Application.UseCases.Users.UserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.User;

[ApiController]
[Route("api/v1/user")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    private readonly UseCaseFetchUserAccountByUserId _useCaseFetchUserAccountByUserId;
    private readonly UseCaseFetchUserPublicationByUser _useCaseFetchUserPublicationByUser;
    private readonly UseCaseFetchUserFriendsByUserId _useCaseFetchUserFriendsByUserId;
    private readonly UseCaseUpdateUserData _useCaseUpdateUserData;
    private readonly UseCaseGetUserInfoByLogin _useCaseGetUserInfoByLogin;
    private readonly UseCaseGetUsersByFilter _useCaseGetUsersByFilter;
    private readonly UseCaseFetchUserProfileByUserId _useCaseFetchUserProfileByUserId;
    private readonly UseCaseGetPublicationsByFilter _useCaseGetPublicationsByFilter;

    public UserController(ILogger<UserController> logger, IConfiguration configuration, TokenService tokenService, UseCaseFetchUserAccountByUserId useCaseFetchUserAccountByUserId, UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, UseCaseFetchUserFriendsByUserId useCaseFetchUserFriendsByUserId, UseCaseUpdateUserData useCaseUpdateUserData, UseCaseGetUserInfoByLogin useCaseGetUserInfoByLogin, UseCaseGetUsersByFilter useCaseGetUsersByFilter, UseCaseFetchUserProfileByUserId useCaseFetchUserProfileByUserId, UseCaseGetPublicationsByFilter useCaseGetPublicationsByFilter)
    {
        _logger = logger;
        _configuration = configuration;
        _tokenService = tokenService;
        
        _useCaseFetchUserAccountByUserId = useCaseFetchUserAccountByUserId;
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _useCaseFetchUserFriendsByUserId = useCaseFetchUserFriendsByUserId;
        _useCaseUpdateUserData = useCaseUpdateUserData;
        _useCaseGetUserInfoByLogin = useCaseGetUserInfoByLogin;
        _useCaseGetUsersByFilter = useCaseGetUsersByFilter;
        _useCaseFetchUserProfileByUserId = useCaseFetchUserProfileByUserId;
        _useCaseGetPublicationsByFilter = useCaseGetPublicationsByFilter;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    
    [HttpGet("discover/users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // Get A list of other Users for the Discover
    public ActionResult<List<DtoOutputUser>> GetUsersByFilter([FromQuery] int userCount, [FromQuery] string? searchValue)
    {
        searchValue ??= "";
        
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseGetUsersByFilter.Execute(data.UserId, userCount, searchValue));
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    [HttpGet("discover/publications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // Get A list of other Users for the Discover
    public ActionResult<List<DtoOutputUser>> GetPublicationsByFilter([FromQuery] int publicationCount, [FromQuery] string? searchValue)
    {
        searchValue ??= "";
        
        try
        {
            var data =  GetAuthCookieData();
            return Ok(_useCaseGetPublicationsByFilter.Execute(data.UserId, publicationCount, searchValue));
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    
    [HttpPut]
    [HttpGet("otherUsers/{login}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
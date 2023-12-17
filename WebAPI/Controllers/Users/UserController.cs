using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.Services.Utils;
using Application.UseCases.Publications;
using Application.UseCases.Users.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Users;

[ApiController]
[Route("api/v1/user")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    private readonly UseCaseFetchUserAccountByUserId _useCaseFetchUserAccountByUserId;
    private readonly UseCaseFetchUserPublicationByUser _useCaseFetchUserPublicationByUser;
    private readonly UseCaseFetchUserFriendsByUserId _useCaseFetchUserFriendsByUserId;
    private readonly UseCaseUpdateUserData _useCaseUpdateUserData;
    private readonly UseCaseGetUsersByFilter _useCaseGetUsersByFilter;
    private readonly UseCaseFetchUserProfileByUserId _useCaseFetchUserProfileByUserId;
    private readonly UseCaseGetPublicationsByFilter _useCaseGetPublicationsByFilter;
    private readonly UseCasePatchUser _useCasePatchUser;
    private readonly UseCaseGetUserPrivacySettings _useCaseGetUserPrivacySettings;
    private readonly UseCaseSetDeletedUser _useCaseSetDeletedUser;
    private readonly UseCaseUpdateUserPassword _useCaseUpdateUserPassword;

    public UserController(IConfiguration configuration, TokenService tokenService, UseCaseFetchUserAccountByUserId useCaseFetchUserAccountByUserId, UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, UseCaseFetchUserFriendsByUserId useCaseFetchUserFriendsByUserId, UseCaseUpdateUserData useCaseUpdateUserData, UseCaseGetUsersByFilter useCaseGetUsersByFilter, UseCaseFetchUserProfileByUserId useCaseFetchUserProfileByUserId, UseCaseGetPublicationsByFilter useCaseGetPublicationsByFilter, UseCasePatchUser useCasePatchUser, UseCaseGetUserPrivacySettings useCaseGetUserPrivacySettings, UseCaseSetDeletedUser useCaseSetDeletedUser, UseCaseUpdateUserPassword useCaseUpdateUserPassword)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        
        _useCaseFetchUserAccountByUserId = useCaseFetchUserAccountByUserId;
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _useCaseFetchUserFriendsByUserId = useCaseFetchUserFriendsByUserId;
        _useCaseUpdateUserData = useCaseUpdateUserData;
        _useCaseGetUsersByFilter = useCaseGetUsersByFilter;
        _useCaseFetchUserProfileByUserId = useCaseFetchUserProfileByUserId;
        _useCaseGetPublicationsByFilter = useCaseGetPublicationsByFilter;
        _useCasePatchUser = useCasePatchUser;
        _useCaseGetUserPrivacySettings = useCaseGetUserPrivacySettings;
        _useCaseSetDeletedUser = useCaseSetDeletedUser;
        _useCaseUpdateUserPassword = useCaseUpdateUserPassword;
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
        catch (Exception)
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
        catch (Exception)
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
    
    [HttpPut]
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
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult PatchUser([FromBody] DtoInputPatchUserPrivacy patch)
    {
        try
        {
            
            _useCasePatchUser.Execute(GetAuthCookieData().UserId, patch);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(error: e.Message);
        }
    }
    
    [HttpGet("privacy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetUserPrivacy()
    {
        try
        {
            return Ok(_useCaseGetUserPrivacySettings.Execute(GetAuthCookieData().UserId));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }

    [HttpPost("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult DeleteUserAccount()
    {
        try
        {
            if (_useCaseSetDeletedUser.Execute(GetAuthCookieData().UserId))
                return NoContent();
            
            return NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    [HttpPost("userPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult UpdateUserPassword([FromBody] DtoInputUpdateUserPassword dtoInputUpdateUserPassword)
    {
        try
        {
            if (!_useCaseUpdateUserPassword.Execute(GetAuthCookieData().UserId, dtoInputUpdateUserPassword))
                return Forbid();
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    [HttpGet("discover/users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    [HttpGet("discover/publications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    
    private (string UserId, int Role) GetAuthCookieData()
    {
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        return _tokenService.GetAuthCookieData(token);
    }
}
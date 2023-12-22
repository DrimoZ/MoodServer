using Application.Dtos.Publication;
using Application.Dtos.User.User;
using Application.Dtos.User.UserProfile;
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
    private readonly UseCaseUpdateUserProfile _useCaseUpdateUserProfile;
    private readonly UseCaseFetchUsersByFilter _useCaseFetchUsersByFilter;
    private readonly UseCaseFetchUserProfileByUserId _useCaseFetchUserProfileByUserId;
    private readonly UseCaseGetPublicationsByFilter _useCaseGetPublicationsByFilter;
    private readonly UseCasePatchUser _useCasePatchUser;
    private readonly UseCaseFetchUserPrivacySettings _useCaseFetchUserPrivacySettings;
    private readonly UseCaseDeleteUser _useCaseDeleteUser;
    private readonly UseCaseUpdateUserPassword _useCaseUpdateUserPassword;
    private readonly UseCaseFetchUserNotifications _useCaseFetchUserNotifications;

    public UserController(
        IConfiguration configuration, TokenService tokenService, 
        UseCaseFetchUserAccountByUserId useCaseFetchUserAccountByUserId, UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, 
        UseCaseFetchUserFriendsByUserId useCaseFetchUserFriendsByUserId, UseCaseUpdateUserProfile useCaseUpdateUserProfile, 
        UseCaseFetchUsersByFilter useCaseFetchUsersByFilter, UseCaseFetchUserProfileByUserId useCaseFetchUserProfileByUserId,
        UseCaseGetPublicationsByFilter useCaseGetPublicationsByFilter, UseCasePatchUser useCasePatchUser, 
        UseCaseFetchUserPrivacySettings useCaseFetchUserPrivacySettings, UseCaseDeleteUser useCaseDeleteUser, 
        UseCaseUpdateUserPassword useCaseUpdateUserPassword, UseCaseFetchUserNotifications useCaseFetchUserNotifications) 
    {
        _configuration = configuration;
        _tokenService = tokenService;
        
        _useCaseFetchUserAccountByUserId = useCaseFetchUserAccountByUserId;
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _useCaseFetchUserFriendsByUserId = useCaseFetchUserFriendsByUserId;
        _useCaseUpdateUserProfile = useCaseUpdateUserProfile;
        _useCaseFetchUsersByFilter = useCaseFetchUsersByFilter;
        _useCaseFetchUserProfileByUserId = useCaseFetchUserProfileByUserId;
        _useCaseGetPublicationsByFilter = useCaseGetPublicationsByFilter;
        _useCasePatchUser = useCasePatchUser;
        _useCaseFetchUserPrivacySettings = useCaseFetchUserPrivacySettings;
        _useCaseDeleteUser = useCaseDeleteUser;
        _useCaseUpdateUserPassword = useCaseUpdateUserPassword;
        _useCaseFetchUserNotifications = useCaseFetchUserNotifications;
    }
    
    // Get the currently connected User Id & Role
    private (string UserId, int UserRole) GetConnectedUserStatus()
    {
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        return _tokenService.GetAuthCookieData(token);
    }

    
    // Get the connected User Id and his Role 
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetUserIdAndRole()
    {
        try
        {
            var status = GetConnectedUserStatus();
            return Ok(new {UserId = status.UserId, UserRole = status.UserRole});
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    // Get the public profile information of given userId
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUserProfile> GetUserProfileByUserId(string userId)
    {
        try
        {
            return Ok(_useCaseFetchUserProfileByUserId.Execute(GetConnectedUserStatus().UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
   
    // Get the public profile account information of given userId
    [HttpGet("{userId}/account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUserAccount> GetUserAccountByUserId(string userId)
    {
        try
        {
            return Ok(_useCaseFetchUserAccountByUserId.Execute(GetConnectedUserStatus().UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    // Get the friends information of given userId
    [HttpGet("{userId}/friends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUserFriends> GetUserFriendsByUserId(string userId)
    {
        try
        {
            return Ok(_useCaseFetchUserFriendsByUserId.Execute(GetConnectedUserStatus().UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    //Get the publications information of given userId
    [HttpGet("{userId}/publications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUserPublications> GetUserPublicationsData(string userId)
    {
        try
        {
            return Ok(_useCaseFetchUserPublicationByUser.Execute(GetConnectedUserStatus().UserId, userId));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult UpdateUserProfile(DtoInputUserUpdateProfile dto)
    {
        try
        {
            if (_useCaseUpdateUserProfile.Execute(dto))
            {
                return NoContent();
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpPatch("privacy")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult PatchUser([FromBody] DtoInputPatchUserPrivacy patch)
    {
        try
        {
            _useCasePatchUser.Execute(GetConnectedUserStatus().UserId, patch);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            return Ok(_useCaseFetchUserPrivacySettings.Execute(GetConnectedUserStatus().UserId));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    // Deletes a User (if the given user is different from the connected user, check if connected has permissions
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult UpdateIsDeleted([FromBody] DtoInputDeleteUser dto)
    {
        try
        {
            var connectedUserId = GetConnectedUserStatus().UserId;

            if (!_useCaseDeleteUser.Execute(connectedUserId, dto.UserId)) return NotFound();
            
            if (connectedUserId != dto.UserId) return NoContent();
            
            if (Request.Cookies.ContainsKey(_configuration["JwtSettings:CookieName"]!))
            {
                Response.Cookies.Delete(_configuration["JwtSettings:CookieName"]!);
            }
            return NoContent();


        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    
    [HttpPost("password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult UpdateUserPassword([FromBody] DtoInputUpdateUserPassword dtoInputUpdateUserPassword)
    {
        try
        {
            if (!_useCaseUpdateUserPassword.Execute(GetConnectedUserStatus().UserId, dtoInputUpdateUserPassword))
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
    public ActionResult<IEnumerable<DtoOutputDiscoverUser>> GetUsersByFilter([FromQuery] int userCount, [FromQuery] string? searchValue)
    {
        searchValue ??= "";
        
        try
        {
            var data =  GetConnectedUserStatus();
            return Ok(_useCaseFetchUsersByFilter.Execute(data.UserId, userCount, searchValue));
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
    public ActionResult<IEnumerable<DtoOutputDiscoverPublication>> GetPublicationsByFilter([FromQuery] int publicationCount, [FromQuery] string? searchValue)
    {
        searchValue ??= "";
        
        try
        {
            var data =  GetConnectedUserStatus();
            return Ok(_useCaseGetPublicationsByFilter.Execute(data.UserId, publicationCount, searchValue));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }


    [HttpGet("notifications")]
    public ActionResult<IEnumerable<DtoOutputNotification>> GetUserNotifications()
    {
        try
        {
            return Ok(_useCaseFetchUserNotifications.Execute(GetConnectedUserStatus().UserId));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
}
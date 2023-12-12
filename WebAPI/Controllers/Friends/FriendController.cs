using Application.Dtos.Account;
using Application.Dtos.Friend;
using Application.Services.Utils;
using Application.UseCases.Friends;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Friends;

[ApiController]
[Route("api/v1/friend")]
public class FriendController:ControllerBase
{

    private readonly UseCaseGetFriendByUserId _useCaseGetFriendByUserId;
    private readonly UseCaseCreateFriend _useCaseCreateFriend;
    private readonly UseCaseDeleteFriend _useCaseDeleteFriend;
    private readonly UseCaseCreateFriendRequest _useCaseCreateFriendRequest;
    
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public FriendController(UseCaseGetFriendByUserId useCaseGetFriendByUserId, UseCaseCreateFriend useCaseCreateFriend, UseCaseDeleteFriend useCaseDeleteFriend, TokenService tokenService, IConfiguration configuration, UseCaseCreateFriendRequest useCaseCreateFriendRequest)
    {
        _useCaseGetFriendByUserId = useCaseGetFriendByUserId;
        _useCaseCreateFriend = useCaseCreateFriend;
        _useCaseDeleteFriend = useCaseDeleteFriend;
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseCreateFriendRequest = useCaseCreateFriendRequest;
    }

    private string GetConnectedUserId()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }

    [HttpGet("{loginFriend}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputFriend> GetFriendByUserId( string loginFriend)
    {
        return _useCaseGetFriendByUserId.Execute(GetConnectedUserId(), loginFriend);
    }
    
    
    
    [HttpPost("/request")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Authorize]
    //When a User click on ADD Button on an other USER
    public ActionResult CreateFriendRequest(string friendId)
    {
        var connectedUserId = GetConnectedUserId();
        if (friendId == connectedUserId) return Unauthorized("Can't be friend with himself");

        try
        {
            var request = _useCaseCreateFriendRequest.Execute(connectedUserId, friendId);
            return StatusCode(201, request);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
        
    }
    
    [HttpPost("/request/accept")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    //When a User click on Accept Button from notifications ?
    public ActionResult AcceptFriendRequest(string friendId)
    {
        return Unauthorized();
    }
    
    [HttpPost("/request/reject")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    //When a User click on Reject Button from notifications ?
    public ActionResult RejectFriendRequest(string friendId)
    {
        return Unauthorized();
    }
    
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    public ActionResult Create(string friendLogin)
    {
        
        var accountCreated = _useCaseCreateFriend.Execute(GetConnectedUserId(), friendLogin);
        return StatusCode(201, accountCreated);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public ActionResult Delete(string friendId)
    {
        if (_useCaseDeleteFriend.Execute(GetConnectedUserId(), friendId))
            return NoContent();
        return NotFound();
    }
}
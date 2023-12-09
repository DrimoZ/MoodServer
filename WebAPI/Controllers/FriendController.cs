using Application.Dtos.Account;
using Application.Dtos.Friend;
using Application.Services.Utils;
using Application.UseCases.Friends;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/friend")]
public class FriendController:ControllerBase
{

    private readonly UseCaseGetFriendByUserId _useCaseGetFriendByUserId;
    private readonly UseCaseCreateFriend _useCaseCreateFriend;
    private readonly UseCaseDeleteFriend _useCaseDeleteFriend;
    
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public FriendController(UseCaseGetFriendByUserId useCaseGetFriendByUserId, UseCaseCreateFriend useCaseCreateFriend, UseCaseDeleteFriend useCaseDeleteFriend, TokenService tokenService, IConfiguration configuration)
    {
        _useCaseGetFriendByUserId = useCaseGetFriendByUserId;
        _useCaseCreateFriend = useCaseCreateFriend;
        _useCaseDeleteFriend = useCaseDeleteFriend;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    private string GetAuthCookieData()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }

    [HttpGet("{loginFriend}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputFriend> GetFriendByUserId( string loginFriend)
    {
        return _useCaseGetFriendByUserId.Execute(GetAuthCookieData(), loginFriend);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputAccount> Create(string friendLogin)
    {
        
        var accountCreated = _useCaseCreateFriend.Execute(GetAuthCookieData(), friendLogin);
        return StatusCode(201, accountCreated);
    }
    
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(string friendId)
    {
        if (_useCaseDeleteFriend.Execute(GetAuthCookieData(), friendId))
            return NoContent();
        return NotFound();
    }
}
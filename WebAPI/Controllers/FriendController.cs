using Application.Dtos.Account;
using Application.Dtos.Friend;
using Application.UseCases.Friends;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/friend")]
public class FriendController:ControllerBase
{

    private readonly UseCaseGetFriendByUserId _useCaseGetFriendByUserId;
    private readonly UseCaseCreateFriend _useCaseCreateFriend;
    private readonly UseCaseDeleteFriend _useCaseDeleteFriend;

    public FriendController(UseCaseGetFriendByUserId useCaseGetFriendByUserId, UseCaseCreateFriend useCaseCreateFriend, UseCaseDeleteFriend useCaseDeleteFriend)
    {
        _useCaseGetFriendByUserId = useCaseGetFriendByUserId;
        _useCaseCreateFriend = useCaseCreateFriend;
        _useCaseDeleteFriend = useCaseDeleteFriend;
    }

    [HttpGet("{userId}/{loginFriend}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputFriend> GetFriendByUserId(string userId, string loginFriend)
    {
        return _useCaseGetFriendByUserId.Execute(userId, loginFriend);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputAccount> Create(string userId, string friendLogin)
    {
        
        var accountCreated = _useCaseCreateFriend.Execute(userId, friendLogin);
        return StatusCode(201, accountCreated);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(string userId, string friendId)
    {
        if (_useCaseDeleteFriend.Execute(userId, friendId))
            return NoContent();
        return NotFound();
    }
}
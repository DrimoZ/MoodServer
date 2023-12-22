using Application.Dtos.Group;
using Application.Dtos.UserGroup;
using Application.Services.Utils;
using Application.UseCases.Groups;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/group")]
[Authorize]
public class GroupController:ControllerBase
{
    private readonly UseCaseCreateGroup _useCaseCreateGroup;
    private readonly UseCaseGetGroupsByUserId _useCaseGetGroupsByUserId;
    private readonly UseCaseGetUserGroupByGroupIdUserId _useCaseGetUserGroupByGroupIdUserId;
    private readonly UseCaseGetUsersFromGroup _useCaseGetUsersFromGroup;
    private readonly UseCaseQuitGroup _useCaseQuitGroup;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly UseCaseGetGroupById _useCaseGetGroupById;
    private readonly UseCaseUpdateGroup _useCaseUpdateGroup;
    private readonly UseCaseUpdateGroupMembers _useCaseUpdateGroupMembers;

    public GroupController(UseCaseCreateGroup useCaseCreateGroup, TokenService tokenService, IConfiguration configuration, UseCaseGetGroupsByUserId useCaseGetGroupsByUserId, UseCaseGetUserGroupByGroupIdUserId useCaseGetUserGroupByGroupIdUserId, UseCaseGetUsersFromGroup useCaseGetUsersFromGroup, UseCaseQuitGroup useCaseQuitGroup, UseCaseGetGroupById useCaseGetGroupById, UseCaseUpdateGroup useCaseUpdateGroup, UseCaseUpdateGroupMembers useCaseUpdateGroupMembers)
    {
        _useCaseCreateGroup = useCaseCreateGroup;
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseGetGroupsByUserId = useCaseGetGroupsByUserId;
        _useCaseGetUserGroupByGroupIdUserId = useCaseGetUserGroupByGroupIdUserId;
        _useCaseGetUsersFromGroup = useCaseGetUsersFromGroup;
        _useCaseQuitGroup = useCaseQuitGroup;
        _useCaseGetGroupById = useCaseGetGroupById;
        _useCaseUpdateGroup = useCaseUpdateGroup;
        _useCaseUpdateGroupMembers = useCaseUpdateGroupMembers;
    }
    
    private string ConnectedUserId()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public ActionResult<DtoOutputGroup> Create(DtoInputCreateGroup group)
    {
        var enumerable = group.UserIds.Append(ConnectedUserId());
        try
        {
            return Ok(_useCaseCreateGroup.Execute(group, enumerable));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status406NotAcceptable, e.Message);
        }
        
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputGroup>> FetchGroupsByUserId()
    {
        return Ok(_useCaseGetGroupsByUserId.Execute(ConnectedUserId()));
    }
    
    [HttpGet("userFromGroup/{groupId:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputGroup>> FetchUsersFromGroup(int groupId)
    {
        return Ok(_useCaseGetUsersFromGroup.Execute( groupId ));
    }
    [HttpGet("{groupId:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputUserGroup>> FetchById(int groupId)
    {
        return Ok(_useCaseGetGroupById.Execute(groupId));
    }
    
    [HttpGet("userGroup/{groupId:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputUserGroup>> FetchUserGroupByGroupIdUserId(int groupId)
    {
        return Ok(_useCaseGetUserGroupByGroupIdUserId.Execute(groupId,ConnectedUserId()));
    }

    [HttpPatch("quitGroup")]
    public ActionResult RemoveUserFromGroup([FromBody] int groupId)
    {
        return Ok(_useCaseQuitGroup.Execute(groupId, ConnectedUserId()));
    }
    [HttpDelete("{groupId:int}/{userId}")]
    public ActionResult RemoveOtherUserFromGroup(int groupId, string userId)
    {
        return Ok(_useCaseQuitGroup.Execute(groupId, userId));
    }
    

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult UpdateIsDeleted(DtoInputUpdateGroup dtoInputUpdateGroup)
    {
        if (_useCaseUpdateGroup.Execute(dtoInputUpdateGroup))
        {
            return Ok();
        }
        return NotFound();
    }
    
    [HttpPost("userGroup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult UptadeGroupMember([FromBody] IEnumerable<DtoInputUserGroup> dtoInputUserGroup)
    {
        return Ok(_useCaseUpdateGroupMembers.Execute(dtoInputUserGroup));
    }
}
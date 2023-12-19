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

    public GroupController(UseCaseCreateGroup useCaseCreateGroup, TokenService tokenService, IConfiguration configuration, UseCaseGetGroupsByUserId useCaseGetGroupsByUserId, UseCaseGetUserGroupByGroupIdUserId useCaseGetUserGroupByGroupIdUserId, UseCaseGetUsersFromGroup useCaseGetUsersFromGroup, UseCaseQuitGroup useCaseQuitGroup, UseCaseGetGroupById useCaseGetGroupById, UseCaseUpdateGroup useCaseUpdateGroup)
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputGroup>> FetchGroupByUserId()
    {
        return Ok(_useCaseGetGroupsByUserId.Execute(GetAuthCookieData()));
    }
    
    [HttpGet("userFromGroup/{groupId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputGroup>> FetchUsersFromGroup(int groupId)
    {
        return Ok(_useCaseGetUsersFromGroup.Execute( groupId ));
    }
    
    [HttpGet("{groupId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputGroup>> FetchGroupByGroupIdUserId(int groupId)
    {
        if (_useCaseUpdateGroup.Execute(dtoInputUpdateGroup))
        {
            return Ok();
        }
        return NotFound();
    }
}
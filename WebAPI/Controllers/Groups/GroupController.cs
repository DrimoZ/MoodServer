using Application.Dtos.Group;
using Application.Services.Utils;
using Application.UseCases.Groups;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/group")]
public class GroupController:ControllerBase
{
    private readonly UseCaseCreateGroup _useCaseCreateGroup;
    private readonly UseCaseGetGroupsByUserId _useCaseGetGroupsByUserId;
    private readonly UseCaseGetUserGroupByGroupIdUserId _useCaseGetUserGroupByGroupIdUserId;
    private readonly UseCaseGetUsersFromGroup _useCaseGetUsersFromGroup;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public GroupController(UseCaseCreateGroup useCaseCreateGroup, TokenService tokenService, IConfiguration configuration, UseCaseGetGroupsByUserId useCaseGetGroupsByUserId, UseCaseGetUserGroupByGroupIdUserId useCaseGetUserGroupByGroupIdUserId, UseCaseGetUsersFromGroup useCaseGetUsersFromGroup)
    {
        _useCaseCreateGroup = useCaseCreateGroup;
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseGetGroupsByUserId = useCaseGetGroupsByUserId;
        _useCaseGetUserGroupByGroupIdUserId = useCaseGetUserGroupByGroupIdUserId;
        _useCaseGetUsersFromGroup = useCaseGetUsersFromGroup;
    }
    
    private string GetAuthCookieData()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputGroup> Create(DtoInputCreateGroup group)
    {
        var enumerable = group.UserIds.Append(GetAuthCookieData());
        return Ok(_useCaseCreateGroup.Execute(group, enumerable));
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
        return Ok(_useCaseGetUserGroupByGroupIdUserId.Execute(groupId,GetAuthCookieData()));
    }
}
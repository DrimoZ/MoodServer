using Application.Dtos.Group;
using Application.Services.Utils;
using Application.UseCases.Groups;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/group")]
public class GroupController:ControllerBase
{
    private readonly UseCaseCreateGroup _useCaseCreateGroup;
    private readonly UseCaseGetGroupsByUserId _useCaseGetGroupsByUserId;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public GroupController(UseCaseCreateGroup useCaseCreateGroup, TokenService tokenService, IConfiguration configuration, UseCaseGetGroupsByUserId useCaseGetGroupsByUserId)
    {
        _useCaseCreateGroup = useCaseCreateGroup;
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseGetGroupsByUserId = useCaseGetGroupsByUserId;
    }
    
    private string GetAuthCookieData()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputGroup> Create(DtoInputCreateGroup group)
    {
        return Ok(_useCaseCreateGroup.Execute(group, group.userIds));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DbGroup>> FetchGroupByUserId()
    {
        return Ok(_useCaseGetGroupsByUserId.Execute(GetAuthCookieData()));
    }
}
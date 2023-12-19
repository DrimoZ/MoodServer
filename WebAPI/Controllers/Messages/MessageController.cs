using Application.Dtos.Message;
using Application.Services.Utils;
using Application.UseCases.Messages;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/message")]
public class MessageController: ControllerBase
{
    private readonly UseCaseCreateMessage _useCaseCreateMessage;
    private readonly UseCaseGetMessageFromGroup _useCaseGetMessageFromGroup;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public MessageController(UseCaseCreateMessage useCaseCreateMessage, UseCaseGetMessageFromGroup useCaseGetMessageFromGroup, TokenService tokenService, IConfiguration configuration)
    {
        _useCaseCreateMessage = useCaseCreateMessage;
        _useCaseGetMessageFromGroup = useCaseGetMessageFromGroup;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    
    private string GetAuthCookieData()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputMessage> Create([FromBody]DtoInputMessage message)
    {
        Console.WriteLine(message.UserGroupId);
        return Ok(_useCaseCreateMessage.Execute(message, message.UserGroupId));
    }
    
    [HttpGet("{groupId:int}/{showCount:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputMessage> FetchByUserGroupId( int  groupId, int showCount)
    {
        return Ok(_useCaseGetMessageFromGroup.Execute(groupId, showCount));
    }
}
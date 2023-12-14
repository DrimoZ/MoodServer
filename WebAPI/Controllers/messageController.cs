using Application.Dtos.Message;
using Application.UseCases.Messages;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/message")]
public class MessageController: ControllerBase
{
    private readonly UseCaseCreateMessage _useCaseCreateMessage;
    private readonly UseCaseGetAllMessageFromGroup _useCaseGetAllMessageFromGroup;

    public MessageController(UseCaseCreateMessage useCaseCreateMessage, UseCaseGetAllMessageFromGroup useCaseGetAllMessageFromGroup)
    {
        _useCaseCreateMessage = useCaseCreateMessage;
        _useCaseGetAllMessageFromGroup = useCaseGetAllMessageFromGroup;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputMessage> Create([FromBody]DtoInputMessage message)
    {
        Console.WriteLine("oui");
        return Ok(_useCaseCreateMessage.Execute(message, message.UserGroupId));
    }
    
    [HttpGet("{groupId:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputMessage> FetchByUserGroupId(int  groupId)
    {
        return Ok(_useCaseGetAllMessageFromGroup.Execute(groupId));
    }
}
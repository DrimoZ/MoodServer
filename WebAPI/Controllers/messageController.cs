using Application.Dtos.Message;
using Application.UseCases.Messages;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/message")]
public class MessageController: ControllerBase
{
    private readonly UseCaseCreateMessage _useCaseCreateMessage;

    public MessageController(UseCaseCreateMessage useCaseCreateMessage)
    {
        _useCaseCreateMessage = useCaseCreateMessage;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputMessage> Create(DtoInputMessage message)
    {
        return Ok(_useCaseCreateMessage.Execute(message, message.UserGroupId));
    }
}
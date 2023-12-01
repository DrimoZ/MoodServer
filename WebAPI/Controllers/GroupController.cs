using Application.Dtos.Group;
using Application.UseCases.Groups;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/group")]
public class GroupController:ControllerBase
{
    private readonly UseCaseCreateGroup _useCaseCreateGroup;

    public GroupController(UseCaseCreateGroup useCaseCreateGroup)
    {
        _useCaseCreateGroup = useCaseCreateGroup;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputGroup> Create(DtoInputCreateGroup group)
    {
        return Ok(_useCaseCreateGroup.Execute(group));
    }
}
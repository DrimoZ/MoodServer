using Application.Dtos.Publication;
using Application.UseCases.Publications;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/publication")]
public class PublicationController: ControllerBase
{
    private readonly UseCaseFetchUserPublicationByUser _useCaseFetchUserPublicationByUser;
    private readonly UseCaseGetPublicationByFriend _useCaseGetPublicationByFriend;
    private readonly UseCaseGetPublicationById _useCaseGetPublicationById;
    private readonly UseCaseCreatePublication _useCaseCreatePublication;
    private readonly UseCaseDeletePublication _useCaseDeletePublication;
    private readonly UseCaseSetPublicationDeleted _useCaseSetPublicationDeleted;

    public PublicationController(UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, UseCaseCreatePublication useCaseCreatePublication, UseCaseDeletePublication useCaseDeletePublication, UseCaseSetPublicationDeleted useCaseSetPublicationDeleted, UseCaseGetPublicationById useCaseGetPublicationById, UseCaseGetPublicationByFriend useCaseGetPublicationByFriend)
    {
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _useCaseGetPublicationByFriend = useCaseGetPublicationByFriend;
        _useCaseCreatePublication = useCaseCreatePublication;
        _useCaseDeletePublication = useCaseDeletePublication;
        _useCaseSetPublicationDeleted = useCaseSetPublicationDeleted;
        _useCaseGetPublicationById = useCaseGetPublicationById;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetPublicationByUserId(string userId)
    {
        return Ok(_useCaseFetchUserPublicationByUser.Execute(userId, userId));
    }
    
    [HttpGet("/newsfeed/{userId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetPublicationByFriend(string userId)
    {
        return Ok(_useCaseGetPublicationByFriend.Execute(userId));
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetPublicationById(int id)
    {
        return Ok(_useCaseGetPublicationById.Execute(id));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputPublication> Create(DtoInputCreatePublication dto)
    {
        var publicationCreated = _useCaseCreatePublication.Execute(dto);
            
        return StatusCode(201, publicationCreated);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
        if (_useCaseDeletePublication.Execute(id))
            return NoContent();
        return NotFound();
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult UpdateIsDeleted(int id, bool isDeleted)
    {
        if (_useCaseSetPublicationDeleted.Execute(id, isDeleted))
        {
            return Ok();
        }

        return NotFound();
    }

}
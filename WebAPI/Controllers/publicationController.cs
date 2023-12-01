using Application.Dtos.Publication;
using Application.UseCases.Publications;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/publication")]
public class PublicationController: ControllerBase
{
    private UseCaseGetPublicationByUser _useCaseGetPublicationByUser;
    private UseCaseCreatePublication _useCaseCreatePublication;
    private UseCaseDeletePublication _useCaseDeletePublication;

    public PublicationController(UseCaseGetPublicationByUser useCaseGetPublicationByUser, UseCaseCreatePublication useCaseCreatePublication, UseCaseDeletePublication useCaseDeletePublication)
    {
        _useCaseGetPublicationByUser = useCaseGetPublicationByUser;
        _useCaseCreatePublication = useCaseCreatePublication;
        _useCaseDeletePublication = useCaseDeletePublication;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetAccountById(string userId)
    {
        return Ok(_useCaseGetPublicationByUser.Execute(userId));
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
    public ActionResult Delete(string id)
    {
        if (_useCaseDeletePublication.Execute(id))
            return NoContent();
        return NotFound();
    }

}
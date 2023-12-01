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

    public PublicationController(UseCaseGetPublicationByUser useCaseGetPublicationByUser, UseCaseCreatePublication useCaseCreatePublication)
    {
        _useCaseGetPublicationByUser = useCaseGetPublicationByUser;
        _useCaseCreatePublication = useCaseCreatePublication;
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
}
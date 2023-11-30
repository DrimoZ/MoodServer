using Application.Dtos.Publication;
using Application.UseCases.Publications;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/publication")]
public class PublicationController: ControllerBase
{
    private UseCaseGetPublicationByUser _useCaseGetPublicationByUser;

    public PublicationController(UseCaseGetPublicationByUser useCaseGetPublicationByUser)
    {
        _useCaseGetPublicationByUser = useCaseGetPublicationByUser;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetAccountById(string userId)
    {
        return Ok(_useCaseGetPublicationByUser.Execute(userId));
    }
}
using Application.Dtos.Publication;
using Application.Services.Utils;
using Application.UseCases.Publications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/publication")]
[Authorize]
public class PublicationController: ControllerBase
{
    private readonly UseCaseFetchUserPublicationByUser _useCaseFetchUserPublicationByUser;
    private readonly UseCaseGetPublicationByFriend _useCaseGetPublicationByFriend;
    private readonly UseCaseGetPublicationById _useCaseGetPublicationById;
    private readonly UseCaseCreatePublication _useCaseCreatePublication;
    private readonly UseCaseDeletePublication _useCaseDeletePublication;
    private readonly UseCaseSetPublicationDeleted _useCaseSetPublicationDeleted;
    
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public PublicationController(UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, UseCaseCreatePublication useCaseCreatePublication, UseCaseDeletePublication useCaseDeletePublication, UseCaseSetPublicationDeleted useCaseSetPublicationDeleted, UseCaseGetPublicationById useCaseGetPublicationById, UseCaseGetPublicationByFriend useCaseGetPublicationByFriend, TokenService tokenService, IConfiguration configuration)
    {
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _useCaseGetPublicationByFriend = useCaseGetPublicationByFriend;
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseCreatePublication = useCaseCreatePublication;
        _useCaseDeletePublication = useCaseDeletePublication;
        _useCaseSetPublicationDeleted = useCaseSetPublicationDeleted;
        _useCaseGetPublicationById = useCaseGetPublicationById;
    }
    
    private string GetConnectedUserId()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }

    /*[HttpGet("{userId}")]
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
    }*/
    
    [HttpGet("{publicationId:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetPublicationById(int publicationId)
    {
        return Ok(_useCaseGetPublicationById.Execute(GetConnectedUserId(), publicationId));
    }
    
    /*[HttpPost]
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
    }*/

}
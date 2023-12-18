using Application.Dtos.Publication;
using Application.Services.Utils;
using Application.UseCases.Publications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Publications;

[ApiController]
[Route("api/v1/publication")]
[Authorize]
public class PublicationController: ControllerBase
{
    private readonly UseCaseFetchUserPublicationByUser _useCaseFetchUserPublicationByUser;
    private readonly UseCaseGetPublicationById _useCaseGetPublicationById;
    private readonly UseCaseLikePublication _useCaseLikePublication;
    private readonly UseCaseCommentPublication _useCaseCommentPublication;
    private readonly UseCaseDeleteCommentInPublicationById _useCaseDeleteCommentInPublicationById;
    private readonly UseCaseGetCommentsByPublicationId _useCaseGetCommentsByPublicationId;
    private readonly UseCaseGetFriendsPublications _useCaseGetFriendsPublications;
    private readonly UseCaseCreatePublication _useCaseCreatePublication;
    private readonly UseCaseDeletePublication _useCaseDeletePublication;
    private readonly UseCaseSetPublicationDeleted _useCaseSetPublicationDeleted;
    
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public PublicationController(UseCaseFetchUserPublicationByUser useCaseFetchUserPublicationByUser, UseCaseCreatePublication useCaseCreatePublication, UseCaseDeletePublication useCaseDeletePublication, UseCaseSetPublicationDeleted useCaseSetPublicationDeleted, UseCaseGetPublicationById useCaseGetPublicationById, TokenService tokenService, IConfiguration configuration, UseCaseLikePublication useCaseLikePublication, UseCaseCommentPublication useCaseCommentPublication, UseCaseDeleteCommentInPublicationById useCaseDeleteCommentInPublicationById, UseCaseGetCommentsByPublicationId useCaseGetCommentsByPublicationId, UseCaseGetFriendsPublications useCaseGetFriendsPublications)
    {
        _useCaseFetchUserPublicationByUser = useCaseFetchUserPublicationByUser;
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseLikePublication = useCaseLikePublication;
        _useCaseCommentPublication = useCaseCommentPublication;
        _useCaseDeleteCommentInPublicationById = useCaseDeleteCommentInPublicationById;
        _useCaseGetCommentsByPublicationId = useCaseGetCommentsByPublicationId;
        _useCaseGetFriendsPublications = useCaseGetFriendsPublications;
        _useCaseCreatePublication = useCaseCreatePublication;
        _useCaseDeletePublication = useCaseDeletePublication;
        _useCaseSetPublicationDeleted = useCaseSetPublicationDeleted;
        _useCaseGetPublicationById = useCaseGetPublicationById;
    }
    
    private string GetConnectedUserId()
    {
        return _tokenService.GetAuthCookieData(HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!).UserId;
    }
    
    [HttpGet("{publicationId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetPublicationById(int publicationId)
    {
        try
        {
            return Ok(_useCaseGetPublicationById.Execute(GetConnectedUserId(), publicationId));
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpGet("friends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPublication> GetFriendsPublications([FromQuery] int publicationCount)
    {
        try
        {
            return Ok(_useCaseGetFriendsPublications.Execute(GetConnectedUserId(), publicationCount));
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpPost("like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult LikePublicationById(DtoInputLikePublication dto)
    {
        try
        {
            _useCaseLikePublication.Execute(GetConnectedUserId(), dto);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost("comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult CommentPublicationById([FromBody] DtoInputCommentPublication dto)
    {
        try
        {
            return Ok(_useCaseCommentPublication.Execute(GetConnectedUserId(), dto));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("{idPublication:int}/comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult GetCommentPublicationById(int idPublication)
    {
        try
        {
            return Ok(_useCaseGetCommentsByPublicationId.Execute(idPublication));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("comment/{idComment:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteCommentPublicationById(int idComment)
    {
        try
        {
            _useCaseDeleteCommentInPublicationById.Execute(GetConnectedUserId(), idComment);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
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
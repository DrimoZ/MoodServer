using Application.Dtos.Images;
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
    private readonly UseCaseGetPublicationById _useCaseGetPublicationById;
    private readonly UseCaseLikePublication _useCaseLikePublication;
    private readonly UseCaseCommentPublication _useCaseCommentPublication;
    private readonly UseCaseDeleteCommentInPublicationById _useCaseDeleteCommentInPublicationById;
    private readonly UseCaseGetCommentsByPublicationId _useCaseGetCommentsByPublicationId;
    private readonly UseCaseGetFriendsPublications _useCaseGetFriendsPublications;
    private readonly UseCaseCreatePublication _useCaseCreatePublication;
    private readonly UseCaseSetPublicationDeleted _useCaseSetPublicationDeleted;
    
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public PublicationController(UseCaseCreatePublication useCaseCreatePublication, UseCaseSetPublicationDeleted useCaseSetPublicationDeleted, UseCaseGetPublicationById useCaseGetPublicationById, TokenService tokenService, IConfiguration configuration, UseCaseLikePublication useCaseLikePublication, UseCaseCommentPublication useCaseCommentPublication, UseCaseDeleteCommentInPublicationById useCaseDeleteCommentInPublicationById, UseCaseGetCommentsByPublicationId useCaseGetCommentsByPublicationId, UseCaseGetFriendsPublications useCaseGetFriendsPublications)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _useCaseLikePublication = useCaseLikePublication;
        _useCaseCommentPublication = useCaseCommentPublication;
        _useCaseDeleteCommentInPublicationById = useCaseDeleteCommentInPublicationById;
        _useCaseGetCommentsByPublicationId = useCaseGetCommentsByPublicationId;
        _useCaseGetFriendsPublications = useCaseGetFriendsPublications;
        _useCaseCreatePublication = useCaseCreatePublication;
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
    
    [HttpPost("post")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public  ActionResult CreateNewPublication ([FromForm] List<IFormFile> images, [FromForm] string description)
    {
        try
        {
            var inputs = new List<DtoInputImage>();

            foreach (var file in images)
            {
                var dtoInput = new DtoInputImage();
                
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                dtoInput.ImageData = memoryStream.ToArray();
                
                inputs.Add(dtoInput);
            }

            _useCaseCreatePublication.Execute(GetConnectedUserId(), inputs, description);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
        
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateIsDeleted([FromBody] int publicationId)
    {
        
        if (_useCaseSetPublicationDeleted.Execute(GetConnectedUserId(), publicationId))
        {
            return Ok();
        }

        return NotFound();
    }

}
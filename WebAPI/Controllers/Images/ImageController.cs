using Application.Dtos.Images;
using Application.Services.Utils;
using Application.UseCases.Images;
using Application.UseCases.Users.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Images;

[ApiController]
[Route("api/v1/image")]
public class ImageController:ControllerBase
{
    private readonly UseCaseGetImageById _useCaseGetImageById;
    private readonly UseCaseUpdateUserProfilePicture _useCaseUpdateUserProfilePicture;
    
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    public ImageController(UseCaseGetImageById useCaseGetImageById, UseCaseUpdateUserProfilePicture useCaseUpdateUserProfilePicture, TokenService tokenService, IConfiguration configuration)
    {
        _useCaseGetImageById = useCaseGetImageById;
        _useCaseUpdateUserProfilePicture = useCaseUpdateUserProfilePicture;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    
    private string GetConnectedUserId()
    {
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        return _tokenService.GetAuthCookieData(token).UserId;
    }
    
    
    [HttpPost("userProfile")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public  ActionResult ChangeUserProfilePicture([FromForm] IFormFile image)
    {
        var connectedUserId = GetConnectedUserId();
        try
        {
            var dtoInput = new DtoInputImage();
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                dtoInput.ImageData = memoryStream.ToArray();
            }

            _useCaseUpdateUserProfilePicture.Execute(connectedUserId, dtoInput);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
        
    }
    
    [HttpGet("{imageId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputImage> GetImageById(int imageId)
    {
        var entity = _useCaseGetImageById.Execute(imageId);
        
        return Ok(entity);
    }
}
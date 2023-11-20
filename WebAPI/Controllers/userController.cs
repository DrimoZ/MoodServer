using Application.Dtos.User;
using Application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController: ControllerBase
{
    private readonly TokenService _tokenService;
    private UseCaseGetAllUsers _useCaseGetAllUsers;

    public UserController(TokenService tokenService, UseCaseGetAllUsers useCaseGetAllUsers)
    {
        _tokenService = tokenService;
        _useCaseGetAllUsers = useCaseGetAllUsers;
    }
    
    
    [HttpGet("isConnected")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult IsConnected()
    {
        //If not connected: blocked on [Authorize] 
        return Ok(StatusCodes.Status200OK);
    }
    
    
    [HttpPost("signIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    public IActionResult SignIn([FromBody] DtoInputSignIn model)
    {
        // Temporaire
        if (!IsUserValid(model.Login, model.Password)) return Unauthorized();
        // Role a recuperer a partir de la db (username - unique + user role)
        var tokenValue = _tokenService.GenerateJwtToken(model.Login, "user");

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        Response.Cookies.Append("MoodSession", tokenValue, cookieOptions);
        
        return Ok();
    }
    
    
    // Temporaire
    private static bool IsUserValid(string login, string password)
    {
        return true;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<DtoOutputUser>> GetAll()
    {
        return Ok(_useCaseGetAllUsers.Execute());
    }
}
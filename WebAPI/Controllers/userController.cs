using Application.Dtos.User;
using Application.UseCases.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController: ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly ILogger<UserController> _logger;
    private UseCaseGetAllUsers _useCaseGetAllUsers;

    public UserController(TokenService tokenService, ILogger<UserController> logger, UseCaseGetAllUsers useCaseGetAllUsers)
    {
        _tokenService = tokenService;
        _logger = logger;
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
        //Check User
        var (isValid, errorMessage) = IsUserValid(model.Login, model.Password);
        if (!isValid)
        {
            return Unauthorized(new { message = errorMessage });
        }
        
        // Role a recuperer a partir de la db (username - unique + user role)
        var tokenValue = _tokenService.GenerateJwtToken(model.Login, "user", !model.StayLoggedIn);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        Response.Cookies.Append("MoodSession", tokenValue, cookieOptions);
        
        return Ok();
    }
    
    [HttpPost("logOut")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public IActionResult Logout()
    {
        try
        {
            // Delete the cookie
            if (Request.Cookies.ContainsKey("MoodSession"))
            {
                Response.Cookies.Delete("MoodSession");
            }

            // Sign out the user
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }
        catch (Exception ex)
        {
            // Log the error
            _logger.LogError(ex, "An error occurred while logging out the user.");

            // Return an error response
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging out the user.");
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<DtoOutputUser>> GetAll()
    {
        return Ok(/*_useCaseGetAllUsers.Execute()*/);
    }
    
    // Temporaire
    private static (bool, string) IsUserValid(string login, string password)
    {
        // Fetch the user from the database
        /*var user = _userService.GetUserByLogin(login);

        if (user == null)
        {
            return (false, "404NotFound");
        }

        if (!user.Password.Equals(password))
        {
            return (false, "401Unauthorized");
        }*/

        return (true, "");
    }
}
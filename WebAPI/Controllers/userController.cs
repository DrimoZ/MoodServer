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
    private readonly UseCaseGetUserByLoginOrMail _useCaseGetUserByLoginOrMail;
    private readonly UseCaseGetUserByName _useCaseGetUserByName;

    public UserController(TokenService tokenService, ILogger<UserController> logger, UseCaseGetUserByLoginOrMail useCaseGetUserByLoginOrMail, UseCaseGetUserByName useCaseGetUserByName)
    {
        _tokenService = tokenService;
        _logger = logger;
        _useCaseGetUserByLoginOrMail = useCaseGetUserByLoginOrMail;
        _useCaseGetUserByName = useCaseGetUserByName;
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult SignIn([FromBody] DtoInputSignIn model)
    {
        //Check User
        var (isValid, errorMessage) = IsUserValid(model.Login, model.Password);
        if (!isValid)
        {
            return NotFound(new
            {
                errorMessage?.Message
            });
        }
        
        // Role a recuperer a partir de la db (username - unique + user role)
        var tokenValue = _tokenService.GenerateJwtToken(model.Login, "user", !model.StayLoggedIn);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
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
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

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
    
    [HttpGet("/getByName/{userName}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<DtoOutputUser>> GetUserByName(string userName)
    {
        return Ok(_useCaseGetUserByName.Execute(userName));
    }
    
    private (bool, Exception?) IsUserValid(string login, string password)
    {
        // Fetch the user from the database
        try
        {
            var user = _useCaseGetUserByLoginOrMail.Execute(login);

            if (!user.Password.Equals(password))
            {
                return (false, new KeyNotFoundException($"Wrong Password for user with login {login}"));
            }
                
            return (true, null);
        }
        catch (KeyNotFoundException e)
        {
            return (false, e);
        }
    }
}
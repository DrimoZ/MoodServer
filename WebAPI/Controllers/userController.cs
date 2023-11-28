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
    private readonly BCryptService _bCryptService;
    
    private readonly ILogger<UserController> _logger;
    
    private readonly UseCaseGetUserByLoginOrMail _useCaseGetUserByLoginOrMail;
    private readonly UseCaseGetUserByLoginAndMail _useCaseGetUserByLoginAndMail;
    private readonly UseCaseGetUserByName _useCaseGetUserByName;
    private readonly UseCaseGetUserByLogin _useCaseGetUserByLogin;
    private readonly UseCaseGetUserByMail _useCaseGetUserByMail;

    public UserController(TokenService tokenService, ILogger<UserController> logger, UseCaseGetUserByLoginOrMail useCaseGetUserByLoginOrMail, UseCaseGetUserByName useCaseGetUserByName, BCryptService bCryptService, UseCaseGetUserByLogin useCaseGetUserByLogin, UseCaseGetUserByMail useCaseGetUserByMail, UseCaseGetUserByLoginAndMail useCaseGetUserByLoginAndMail)
    {
        _tokenService = tokenService;
        _logger = logger;
        _useCaseGetUserByLoginOrMail = useCaseGetUserByLoginOrMail;
        _useCaseGetUserByName = useCaseGetUserByName;
        _bCryptService = bCryptService;
        _useCaseGetUserByLogin = useCaseGetUserByLogin;
        _useCaseGetUserByMail = useCaseGetUserByMail;
        _useCaseGetUserByLoginAndMail = useCaseGetUserByLoginAndMail;
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
        try
        {
            // Get User From Database
            var dbUser = _useCaseGetUserByLoginOrMail.Execute(model.Login);
            
            // Check If Given Password Corresponds
            if (!_bCryptService.VerifyPassword(model.Password, dbUser.Password)) return NotFound();

            // Try Generating a Token and publish it
            if (GenerateToken(model.Login, dbUser.Role.ToString(), model.StayLoggedIn)) return Ok();
            return NotFound();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult SignUp([FromBody] DtoInputSignUp model)
    {
        try
        {
            // Check if Login or Mail is already existing
            _useCaseGetUserByLoginAndMail.Execute(model.Login, model.Mail);
            return Conflict();
        }
        catch (KeyNotFoundException)
        {
            //Create account
        
            //Create User
        
            //Create Token
            
            return Ok();
        }
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
    
    [HttpGet("{userName}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<DtoOutputUser>> GetUserByName(string userName)
    {
        return Ok(_useCaseGetUserByName.Execute(userName));
    }

    private bool GenerateToken(string login, string role, bool isSessionOnly)
    {
        try
        {
            var tokenValue = _tokenService.GenerateJwtToken(login, role, isSessionOnly);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("MoodSession", tokenValue, cookieOptions);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
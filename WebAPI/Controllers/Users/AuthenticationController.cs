using Application.Dtos.User.UserAuthentication;
using Application.Services.Utils;
using Application.UseCases.Users.UserAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Users;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController: ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    private readonly UseCaseGetUserByLoginOrMail _useCaseGetUserByLoginOrMail;
    private readonly UseCaseGetUserByLoginAndMail _useCaseGetUserByLoginAndMail;
    private readonly UseCaseCreateUser _useCaseCreateUser;

    public AuthenticationController(TokenService tokenService, UseCaseCreateUser useCaseCreateUser, UseCaseGetUserByLoginOrMail useCaseGetUserByLoginOrMail, UseCaseGetUserByLoginAndMail useCaseGetUserByLoginAndMail, IConfiguration configuration)
    {
        _tokenService = tokenService;
        
        _useCaseCreateUser = useCaseCreateUser;
        _useCaseGetUserByLoginOrMail = useCaseGetUserByLoginOrMail;
        _useCaseGetUserByLoginAndMail = useCaseGetUserByLoginAndMail;
        _configuration = configuration;
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
    public IActionResult UserSignIn([FromBody] DtoInputSignInUser model)
    {
        try
        {
            // Get User From Database
            var dbUser = _useCaseGetUserByLoginOrMail.Execute(model.Login);
            // Check If Given Password Corresponds
            if (!BCryptService.VerifyPassword(model.Password, dbUser.Password)) return NotFound();

            // Try Generating a Token and publish it
            if (!GenerateToken(dbUser.Id, dbUser.Role.ToString(), !model.StayLoggedIn)) return NotFound();
            
            
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult UserSignUp([FromBody] DtoInputSignUpUser model)
    {
        try
        {
            // Check if Login or Mail is already existing
            _useCaseGetUserByLoginAndMail.Execute(model.Login, model.Mail);
            return Conflict();
        }
        catch (KeyNotFoundException)
        {
            try
            {
                //Create User
                var dbCreatedUser = _useCaseCreateUser.Execute(model);
                
                //Verify Password has not changed
                if (!BCryptService.VerifyPassword(model.Password, dbCreatedUser.Password))
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user account.");
                
                // Try Generating a Token and publish it
                if (!GenerateToken(dbCreatedUser.Id, dbCreatedUser.Role.ToString(), true)) return NotFound();
                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
    }
    
    [HttpPost("signOut")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public IActionResult UserSignOut()
    {
        try
        {
            // Delete the cookie
            if (Request.Cookies.ContainsKey(_configuration["JwtSettings:CookieName"]!))
            {
                Response.Cookies.Delete(_configuration["JwtSettings:CookieName"]!);
            }

            return Ok();
        }
        catch (Exception)
        {
            // Return an error response
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging out the user.");
        }
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
        catch (Exception)
        {
            return false;
        }
    }
}
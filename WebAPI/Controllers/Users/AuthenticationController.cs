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
    
    private readonly UseCaseVerifySignInUser _useCaseVerifySignInUser;
    private readonly UseCaseVerifySignUpUser _useCaseVerifySignUpUser;
    private readonly UseCaseCreateUser _useCaseCreateUser;

    public AuthenticationController(TokenService tokenService, UseCaseCreateUser useCaseCreateUser, UseCaseVerifySignInUser useCaseVerifySignInUser, UseCaseVerifySignUpUser useCaseVerifySignUpUser, IConfiguration configuration)
    {
        _tokenService = tokenService;
        
        _useCaseCreateUser = useCaseCreateUser;
        _useCaseVerifySignInUser = useCaseVerifySignInUser;
        _useCaseVerifySignUpUser = useCaseVerifySignUpUser;
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
    public IActionResult UserSignIn([FromBody] DtoInputUserSignIn inputUserSignIn)
    {
        try
        {
            // Get User From Database
            var dtoUser = _useCaseVerifySignInUser.Execute(inputUserSignIn.UserLogin);
            
            // Check If Given Password Corresponds
            if (!BCryptService.VerifyPassword(inputUserSignIn.UserPassword, dtoUser.UserPassword)) return NotFound();

            // Try Generating a Token and publish it
            if (!GenerateToken(dtoUser.UserId, dtoUser.UserRole.ToString(), !inputUserSignIn.StayLoggedIn)) return NotFound();
            
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public IActionResult UserSignUp([FromBody] DtoInputUserSignUp model)
    {
        try
        {
            // Check if Login or Mail is already existing
            _useCaseVerifySignUpUser.Execute(model.UserLogin, model.UserMail);
            return Conflict();
        }
        catch (KeyNotFoundException)
        {
            try
            {
                //Create User
                var dtoUser = _useCaseCreateUser.Execute(model);
                
                //If User is not created
                if (dtoUser == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user account.");
                
                //Verify Password has not changed
                if (!BCryptService.VerifyPassword(model.UserPassword, dtoUser.UserPassword))
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user account.");
                
                // Try Generating a Token and publish it
                if (!GenerateToken(dtoUser.UserId, dtoUser.UserRole.ToString(), true)) return NotFound();
                
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
            
            if (!isSessionOnly)
            {
                cookieOptions.Expires = DateTimeOffset.UtcNow.AddHours(int.Parse(_configuration["JwtSettings:ValidityHours"]!));
            }

            Response.Cookies.Append("MoodSession", tokenValue, cookieOptions);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
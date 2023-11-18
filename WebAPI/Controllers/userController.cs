using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
public class UserController: ControllerBase
{
    [HttpGet("IsConnected")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult IsConnected()
    {
        // If the code execution reaches here, it means the user is authenticated.
        // You can return a 200 OK response to indicate that.

        return Ok();
    }
}
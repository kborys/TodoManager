using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Models.Authentication;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate(LoginRequest request)
    {
        var response = await _userService.Authenticate(request);

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var newUser = await _userService.Create(request);

        return CreatedAtRoute("GetUserById", new { id = newUser.UserId }, newUser);
    }
}

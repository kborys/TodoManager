using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Models.Users;
using System.Security.Claims;
using TodoManager.Common.Contracts.Services;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserCreateRequest request)
    {
        var newUser = await _userService.Create(request);

        return CreatedAtRoute("GetUserById", new { id = newUser.UserId }, newUser);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<ActionResult<User?>> GetById(int id)
    {
        var user = await _userService.GetById(id);

        return Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UserUpdateRequest request)
    {
        if(!UserIsActiveUser(id))
            return Forbid();

        await _userService.Update(id, request);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if(!UserIsActiveUser(id))
            return Forbid();

        await _userService.Delete(id);

        return NoContent();
    }

    private bool UserIsActiveUser(int userId)
    {
        int activeUserId = _userService.GetActiveUserId();
        return activeUserId == userId;
    }
}

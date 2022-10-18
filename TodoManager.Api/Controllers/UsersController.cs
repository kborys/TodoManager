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
    private readonly IUserService _users;

    public UsersController(IUserService usersService)
    {
        _users = usersService;
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest request)
    {
        var response = await _users.Authenticate(request);

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CreateRequest request)
    {
        var newUser = await _users.Create(request);

        return CreatedAtRoute("GetUserById", new { id = newUser.UserId }, newUser);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<ActionResult<User?>> GetById(int id)
    {
        var user = await _users.GetById(id);

        return Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateRequest request)
    {
        if(!UserIsActiveUser(id))
            return Forbid();

        await _users.Update(id, request);

        return Ok(new { message = "User updated successfully"});
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if(!UserIsActiveUser(id))
            return Forbid();

        await _users.Delete(id);

        return Ok(new { message = "User deleted successfully"});
    }

    private bool UserIsActiveUser(int userId)
    {
        return GetActiveUserId() == userId;
    }

    private int GetActiveUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    
        return int.Parse(userIdText!);
    }
}

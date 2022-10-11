using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Entities;
using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;
using System.Security.Claims;

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
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
    {
        var response = await _users.Authenticate(model);

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CreateRequest model)
    {
        await _users.Create(model);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User?>> GetById(int id)
    {
        return await _users.GetById(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateRequest model)
    {
        var activeUserId = GetActiveUserId();
        if(activeUserId != id)
            return Unauthorized();

        await _users.Update(id, model);

        return Ok(new { message = "User updated successfully"});
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var activeUserId = GetActiveUserId();
        if(activeUserId != id)
            return Unauthorized();

        await _users.Delete(id);

        return Ok(new { message = "User deleted successfully"});
    }

    private int GetActiveUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return int.Parse(userIdText!);
    }
}

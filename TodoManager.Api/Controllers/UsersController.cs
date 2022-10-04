using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Entities;
using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;

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
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var response = _users.Authenticate(model);

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(CreateRequest model)
    {
        _users.Create(model);

        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<User?> GetById(int id)
    {
        return _users.GetById(id);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateRequest model)
    {
        _users.Update(id, model);

        return Ok(new { message = "User updated successfully"});
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _users.Delete(id);

        return Ok(new { message = "User deleted successfully"});
    }
}

using Microsoft.AspNetCore.Mvc;
using TodoManager.Api.Helpers;
using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Models.Users;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthHelper _authHelper;

    public UsersController(IUserService userService, IAuthHelper authHelper)
    {
        _userService = userService;
        _authHelper = authHelper;
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
        var activeUserId = _authHelper.GetActiveUserId();
        await _userService.Update(id, request, activeUserId);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        await _userService.Delete(id, activeUserId);

        return NoContent();
    }
}

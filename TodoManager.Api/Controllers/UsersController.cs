using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Models.Users;
using TodoManager.Common.Contracts.Services;
using TodoManager.Api.Helpers;

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

    [HttpPost("Authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("Register")]
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

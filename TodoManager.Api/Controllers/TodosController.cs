using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Api.Controllers;

[Route("api/Groups/{groupId:int}/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IUserService _userService;

    public TodosController(ITodoService todoService, IUserService userService)
    {
        _todoService = todoService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll(int groupId)
    {
        int activeUserId = _userService.GetActiveUserId();
        var todos = await _todoService.GetAllByGroup(activeUserId, groupId);

        return Ok(todos);
    }
}

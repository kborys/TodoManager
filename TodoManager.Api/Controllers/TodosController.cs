using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
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

    [HttpGet("{todoId:int}", Name = "GetTodoById")]
    public async Task<ActionResult<Todo?>> GetById(int todoId)
    {
        var todo = await _todoService.GetById(todoId);

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> Create(TodoCreateRequest request)
    {
        var activeUserId = _userService.GetActiveUserId();
        if (activeUserId != request.OwnerId)
            return BadRequest();
        var newTodo = await _todoService.Create(request);

        return CreatedAtRoute("GetTodoById", new { todoId = newTodo.TodoId }, newTodo);
    }
}

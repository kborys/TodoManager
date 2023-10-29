using Microsoft.AspNetCore.Mvc;
using TodoManager.Api.Helpers;
using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Models.Enums;
using TodoManager.Application.Models.Todos;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IAuthHelper _authHelper;

    public TodosController(ITodoService todoService, IAuthHelper authHelper)
    {
        _todoService = todoService;
        _authHelper = authHelper;
    }

    [HttpGet("{todoId:int}", Name = "GetTodoById")]
    public async Task<ActionResult<Todo?>> GetById(int todoId)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var todo = await _todoService.GetById(todoId, activeUserId);

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> Create(TodoCreateRequest request)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var newTodo = await _todoService.Create(request, activeUserId);

        return CreatedAtRoute("GetTodoById", new { todoId = newTodo.TodoId }, newTodo);
    }

    [HttpPut("{todoId:int}")]
    public async Task<ActionResult> Update(int todoId, [FromBody] Status status)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        await _todoService.UpdateStatus(todoId, status, activeUserId);

        return NoContent();
    }

}

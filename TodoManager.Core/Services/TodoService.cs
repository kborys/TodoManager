using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Core.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IGroupService _groupService;

    public TodoService(ITodoRepository todoRepository, IGroupService groupService)
    {
        _todoRepository = todoRepository;
        _groupService = groupService;
    }


    public async Task<Todo> Create(TodoCreateRequest request)
    {
        await _groupService.CheckMembership(request.GroupId);

        var newTodoId = await _todoRepository.Create(request);
        var newTodo = new Todo(newTodoId, request.Title, request.GroupId, request.OwnerId, request.Status,
            request.Description);

        return newTodo;
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int groupId)
    {
        await _groupService.CheckMembership(groupId);

        return await _todoRepository.GetAllByGroup(groupId);
    }

    public async Task<Todo?> GetById(int todoId)
    {
        var todo = await _todoRepository.GetById(todoId);
        if(todo is null) return null;
        await _groupService.CheckMembership(todo.GroupId);

        return todo;
    }
}

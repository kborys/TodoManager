using TodoManager.Application.Models.Todos;
using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Models.Enums;
using TodoManager.Application.Interfaces.Repositories;
using TodoManager.Application.Exceptions;

namespace TodoManager.Application.Services;

internal class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IGroupService _groupService;

    public TodoService(ITodoRepository todoRepository, IGroupService groupService)
    {
        _todoRepository = todoRepository;
        _groupService = groupService;
    }


    public async Task<Todo> Create(TodoCreateRequest createTodoRequest, int requesteeId)
    {
        var isMember = await _groupService.IsGroupMember(createTodoRequest.GroupId, requesteeId);
        if (!isMember)
            throw new NotMemberException();

        var createdTodoId = await _todoRepository.Create(createTodoRequest);
        var createdTodo = new Todo(createdTodoId, createTodoRequest.Title, createTodoRequest.GroupId, createTodoRequest.OwnerId, createTodoRequest.Status, createTodoRequest.Description);

        return createdTodo;
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int groupId, int requesteeId)
    {
        var isMember = await _groupService.IsGroupMember(groupId, requesteeId);
        if (!isMember)
            throw new NotMemberException();

        return await _todoRepository.GetAllByGroup(groupId);
    }

    public async Task<Todo?> GetById(int todoId, int requesteeId)
    {
        var todo = await _todoRepository.GetById(todoId);
        if (todo is null)
            return null;

        var isMember = await _groupService.IsGroupMember(todo.GroupId, requesteeId);
        if (!isMember)
            throw new NotMemberException();

        return todo;
    }

    public async Task UpdateStatus(int todoId, Status status, int requesteeId)
    {
        var todo = await _todoRepository.GetById(todoId)
            ?? throw new NotFoundException("Todo with given id does not exist");
        var isMember = await _groupService.IsGroupMember(todo.GroupId, requesteeId);
        if (!isMember)
            throw new NotMemberException();

        todo.Status = status;
        await _todoRepository.Update(todo);
    }
}
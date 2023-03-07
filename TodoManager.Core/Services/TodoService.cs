using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Exceptions;
using TodoManager.Common.Models.Enums;
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


    public async Task<Todo> Create(TodoCreateRequest request, int activeUserId)
    {
        var isMember = await _groupService.IsGroupMember(request.GroupId, activeUserId);
        if (!isMember)
            throw new NotMemberException();

        var newTodoId = await _todoRepository.Create(request);
        var newTodo = new Todo(newTodoId, request.Title, request.GroupId, request.OwnerId, request.Status,
            request.Description);

        return newTodo;
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int groupId, int activeUserId)
    {
        var isMember = await _groupService.IsGroupMember(groupId, activeUserId);
        if (!isMember)
            throw new NotMemberException();

        return await _todoRepository.GetAllByGroup(groupId);
    }

    public async Task<Todo?> GetById(int todoId, int activeUserId)
    {
        var todo = await _todoRepository.GetById(todoId);
        if (todo is not null)
        {
            var isMember = await _groupService.IsGroupMember(todo.GroupId, activeUserId);
            if (!isMember)
                throw new NotMemberException();
        }

        return todo;
    }

    public async Task UpdateStatus(int todoId, Status status, int activeUserId)
    {
        var todo = await _todoRepository.GetById(todoId);
        if (todo is null)
            throw new NotFoundException("Todo with given id does not exist");

        var isMember = await _groupService.IsGroupMember(todo.GroupId, activeUserId);
        if (!isMember)
            throw new NotMemberException();

        todo.Status = status;
        await _todoRepository.Update(todo);
    }
}
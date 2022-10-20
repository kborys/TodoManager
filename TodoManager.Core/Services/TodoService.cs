using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Core.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<Todo> Create(TodoCreateRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int activeUserId, int groupId)
    {
        return await _todoRepository.GetAllByGroup(groupId);
    }
}

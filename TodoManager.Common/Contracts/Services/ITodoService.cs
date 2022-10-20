using TodoManager.Common.Models.Todos;

namespace TodoManager.Common.Contracts.Services;

public interface ITodoService
{
    public Task<Todo> Create(TodoCreateRequest request);
    public Task<IEnumerable<Todo>> GetAllByGroup(int activeUserId, int groupId);
}

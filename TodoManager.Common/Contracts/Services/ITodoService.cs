using TodoManager.Common.Models.Enums;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Common.Contracts.Services;

public interface ITodoService
{
    Task<Todo> Create(TodoCreateRequest request, int activeUserId);
    Task<IEnumerable<Todo>> GetAllByGroup(int groupId, int activeUserId);
    Task<Todo?> GetById(int todoId, int activeUserId);
    Task UpdateStatus(int todoId, Status status, int activeUserId);
}

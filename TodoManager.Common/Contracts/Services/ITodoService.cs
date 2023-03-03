using TodoManager.Common.Models.Enums;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Common.Contracts.Services;

public interface ITodoService
{
    Task<Todo> Create(TodoCreateRequest request);
    Task<IEnumerable<Todo>> GetAllByGroup(int groupId);
    Task<Todo?> GetById(int todoId);
    Task UpdateStatus(int todoId, Status status);
}

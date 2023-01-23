using TodoManager.Common.Models.Todos;

namespace TodoManager.Common.Contracts.Repositories;

public interface ITodoRepository
{
    Task<int> Create(TodoCreateRequest request);
    Task<IEnumerable<Todo>> GetAllByGroup(int groupId);
    Task<Todo?> GetById(int todoId);
    Task Delete(int todoId);
}

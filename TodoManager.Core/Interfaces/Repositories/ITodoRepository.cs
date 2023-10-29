using TodoManager.Application.Models.Todos;

namespace TodoManager.Application.Interfaces.Repositories;

public interface ITodoRepository
{
    Task<int> Create(TodoCreateRequest request);
    Task<IEnumerable<Todo>> GetAllByGroup(int groupId);
    Task<Todo?> GetById(int todoId);
    Task Update(Todo todo);
    Task Delete(int todoId);
}

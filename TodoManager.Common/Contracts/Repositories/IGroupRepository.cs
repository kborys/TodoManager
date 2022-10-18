using TodoManager.Common.Models.Groups;

namespace TodoManager.Common.Contracts.Repositories;

public interface IGroupRepository
{
    Task<int> Create(CreateRequest request);
    Task<Group?> GetById(int id);
    Task Update(Group group);
    Task Delete(int id);
    Task<int> Count(int id);
}

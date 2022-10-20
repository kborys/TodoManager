using TodoManager.Common.Models.Groups;

namespace TodoManager.Common.Contracts.Repositories;

public interface IGroupRepository
{
    Task<int> Create(GroupCreateRequest request);
    Task<IEnumerable<Group>> GetAllByUser(int userId);
    Task Delete(int groupId);
}

using TodoManager.Common.Models.Groups;

namespace TodoManager.Common.Contracts.Services;

public interface IGroupService
{
    Task<Group> Create(GroupCreateRequest request);
    Task<IEnumerable<Group>> GetAllByUser(int userId);
}
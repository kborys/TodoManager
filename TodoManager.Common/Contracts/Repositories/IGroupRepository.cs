using TodoManager.Common.Models.Groups;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Repositories;

public interface IGroupRepository
{
    Task<int> Create(GroupCreateRequest request, int requestedBy);
    Task<IEnumerable<Group>> GetAllByUser(int userId);
    Task<Group?> GetById(int groupId);
    Task AssignUser (int userId, int groupId);
    Task Delete(int groupId);
    Task Update(GroupUpdateRequest request, int groupId);
    Task<IEnumerable<User>> GetGroupMembers(int groupId);
}

using TodoManager.Common.Models.Groups;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Services;

public interface IGroupService
{
    Task<Group> Create(GroupCreateRequest request, int requestedBy);
    Task<IEnumerable<Group>> GetAllByUser(int userId);
    Task<Group?> GetById(int activeUserId, int groupId);
    Task AssignUser(int userId, int activeUserId, int groupId);
    Task Delete(int activeUserId, int groupId);
    Task Update(GroupUpdateRequest request, int activeUserId, int groupId);
    Task<IEnumerable<User>> GetGroupMembers(int groupId);
}
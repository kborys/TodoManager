using TodoManager.Common.Models.Groups;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Services;

public interface IGroupService
{
    Task<Group> Create(GroupCreateRequest request, int activeUserId);
    Task<IEnumerable<Group>> GetAllByUser(int activeUserId);
    Task<Group?> GetById(int groupId, int activeUserId);
    Task AddMember(int userId, int groupId, int activeUserId);
    Task RemoveMember(int userId, int groupId, int activeUserId);

    Task Delete(int groupId, int activeUserId);
    Task Update(GroupUpdateRequest request, int groupId, int activeUserId);
    Task<IEnumerable<User>> GetGroupMembers(int groupId, int activeUserId);
    Task<bool> IsGroupMember(int groupId, int userId);
}
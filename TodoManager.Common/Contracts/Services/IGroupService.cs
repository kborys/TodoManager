using TodoManager.Common.Models.Groups;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Services;

public interface IGroupService
{
    Task<Group> Create(GroupCreateRequest request);
    Task<IEnumerable<Group>> GetAllByUser(int userId);
    Task<Group?> GetById(int groupId);
    Task AssignUser(int userId, int groupId);
    Task Delete(int groupId);
    Task Update(GroupUpdateRequest request, int groupId);
    Task<IEnumerable<User>> GetGroupMembers(int groupId);
    Task CheckOwnership(int groupId);
    Task CheckMembership(int groupId);
}
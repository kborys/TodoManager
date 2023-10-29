using TodoManager.Application.Models.Groups;
using TodoManager.Application.Models.Users;

namespace TodoManager.Application.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<int> Create(GroupCreateRequest request, int requestedBy);
    Task<IEnumerable<Group>> GetAllByUser(int userId);
    Task<Group?> GetById(int groupId);
    Task AddMember(int userId, int groupId);
    Task RemoveMember(int userId, int groupId);
    Task Delete(int groupId);
    Task Update(GroupUpdateRequest request, int groupId);
    Task<IEnumerable<User>> GetGroupMembers(int groupId);
}

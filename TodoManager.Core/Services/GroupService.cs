using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Groups;
using TodoManager.Common.Models.Users;

namespace TodoManager.Core.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task AssignUser(int userId, int activeUserId, int groupId)
    {
        await CheckOwnership(activeUserId, groupId);

        await _groupRepository.AssignUser(userId, groupId);
    }

    public async Task<Group> Create(GroupCreateRequest request, int activeUserId)
    {
        var newGroupId = await _groupRepository.Create(request, activeUserId);
        var newGroup = new Group(request.Name, activeUserId, newGroupId);

        return newGroup;
    }

    public async Task Delete(int activeUserId, int groupId)
    {
        await CheckOwnership(activeUserId, groupId);

        await _groupRepository.Delete(groupId);
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int userId)
    {
        return await _groupRepository.GetAllByUser(userId);
    }

    public async Task<Group?> GetById(int activeUserId, int groupId)
    {
        return await _groupRepository.GetById(activeUserId, groupId);
    }

    public async Task<IEnumerable<User>> GetGroupMembers(int groupId)
    {
        return await _groupRepository.GetGroupMembers(groupId);
    }

    public async Task Update(GroupUpdateRequest request, int activeUserId, int groupId)
    {
        await CheckOwnership(activeUserId, groupId);

        await _groupRepository.Update(request, groupId);
    }

    private async Task CheckOwnership(int userId, int groupId)
    {
        var group = await _groupRepository.GetById(userId, groupId);

        if(group?.OwnerId != userId)
            throw new UnauthorizedAccessException("You must be the group owner in order to do this.");
    }
}

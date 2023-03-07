using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Exceptions;
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

    public async Task AssignUser(int userId, int groupId, int activeUserId)
    {
        var isMember = await IsGroupMember(groupId, activeUserId);
        if (!isMember)
            throw new NotMemberException();

        await _groupRepository.AssignUser(userId, groupId);
    }

    public async Task<Group> Create(GroupCreateRequest request, int activeUserId)
    {
        var newGroupId = await _groupRepository.Create(request, activeUserId);
        var newGroup = new Group(request.Name, activeUserId, newGroupId);

        return newGroup;
    }

    public async Task Delete(int groupId, int activeUserId)
    {
        var isOwner = await IsGroupOwner(groupId, activeUserId);
        if (!isOwner)
            throw new NotOwnerException();

        await _groupRepository.Delete(groupId);
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int activeUserId)
    {
        return await _groupRepository.GetAllByUser(activeUserId);
    }

    public async Task<Group?> GetById(int groupId, int activeUserId)
    {
        var isMember = await IsGroupMember(groupId, activeUserId);
        if (!isMember)
            throw new NotMemberException();
        return await _groupRepository.GetById(groupId);
    }

    public async Task<IEnumerable<User>> GetGroupMembers(int groupId, int activeUserId)
    {
        var isMember = await IsGroupMember(groupId, activeUserId);
        if(!isMember)
            throw new NotMemberException();

        return await _groupRepository.GetGroupMembers(groupId);
    }

    public async Task Update(GroupUpdateRequest request, int groupId, int activeUserId)
    {
        var isOwner = await IsGroupOwner(groupId, activeUserId);
        if (!isOwner)
            throw new NotOwnerException();
        await _groupRepository.Update(request, groupId);
    }

    public async Task<bool> IsGroupOwner(int groupId, int userId)
    {
        var group = await _groupRepository.GetById(groupId);
        if (group is null)
            throw new GroupNotFoundException();

        return group.OwnerId == userId;
    }

    public async Task<bool> IsGroupMember(int groupId, int userId)
    {
        var groupMembers = await _groupRepository.GetGroupMembers(groupId);
        var isMember = groupMembers.Any(x => x.UserId == userId);
        return isMember;
    }
}

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

    public async Task AddMember(int subjectId, int groupId, int requesteeId)
    {
        var requesteeIsGroupMember = await IsGroupMember(groupId, requesteeId);
        if (!requesteeIsGroupMember)
            throw new NotMemberException();
        
        var subjectUserIsAlreadyGroupMember = await IsGroupMember(groupId, subjectId);
        if (subjectUserIsAlreadyGroupMember)
            return;

        await _groupRepository.AddMember(subjectId, groupId);
    }

    public async Task RemoveMember(int subjectId, int groupId, int requesteeId)
    {
        var requesteeIsGroupMember = await IsGroupMember(groupId, requesteeId);
        if (!requesteeIsGroupMember)
            throw new NotMemberException();

        var group = await _groupRepository.GetById(groupId);
        if (group?.OwnerId != requesteeId)
            throw new UnauthorizedAccessException("You can't kick out the group owner.");


        await _groupRepository.RemoveMember(subjectId, groupId);
    }

    public async Task<Group> Create(GroupCreateRequest createGroupRequest, int requesteeId)
    {
        var newGroupId = await _groupRepository.Create(createGroupRequest, requesteeId);
        var newGroup = new Group(createGroupRequest.Name, requesteeId, newGroupId);

        return newGroup;
    }

    public async Task Delete(int groupId, int requesteeId)
    {
        var group = await _groupRepository.GetById(groupId);
        if (group is null) return;
        if (group?.OwnerId != requesteeId)
            throw new NotOwnerException();

        await _groupRepository.Delete(groupId);
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int requesteeId)
    {
        return await _groupRepository.GetAllByUser(requesteeId);
    }

    public async Task<Group?> GetById(int groupId, int requesteeId)
    {
        var requesteeIsGroupMember = await IsGroupMember(groupId, requesteeId);
        if (!requesteeIsGroupMember)
            throw new NotMemberException();

        return await _groupRepository.GetById(groupId);
    }

    public async Task<IEnumerable<User>> GetGroupMembers(int groupId, int requesteeId)
    {
        var groupMembers = await _groupRepository.GetGroupMembers(groupId);
        if (!groupMembers.Any(u => u.UserId == requesteeId))
            throw new NotMemberException();

        return groupMembers;
    }

    public async Task Update(GroupUpdateRequest request, int groupId, int requesteeId)
    {
        var group = await _groupRepository.GetById(groupId);
        if (group is null) return;
        if (group.OwnerId != requesteeId)
            throw new NotOwnerException();
        
        await _groupRepository.Update(request, groupId);
    }

    public async Task<bool> IsGroupMember(int groupId, int userId)
    {
        var groupMembers = await _groupRepository.GetGroupMembers(groupId);
        var isMember = groupMembers.Any(x => x.UserId == userId);
        return isMember;
    }
}

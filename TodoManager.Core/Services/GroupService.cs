using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Groups;
using TodoManager.Common.Models.Users;

namespace TodoManager.Core.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserService _userService;

    public GroupService(IGroupRepository groupRepository, IUserService userService)
    {
        _groupRepository = groupRepository;
        _userService = userService;
    }

    public async Task AssignUser(int userId, int groupId)
    {
        await CheckMembership(groupId);
        await _groupRepository.AssignUser(userId, groupId);
    }

    public async Task<Group> Create(GroupCreateRequest request)
    {
        var activeUserId = _userService.GetActiveUserId();
        var newGroupId = await _groupRepository.Create(request, activeUserId);
        var newGroup = new Group(request.Name, activeUserId, newGroupId);

        return newGroup;
    }

    public async Task Delete(int groupId)
    {
        await CheckOwnership(groupId);
        await _groupRepository.Delete(groupId);
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int userId)
    {
        return await _groupRepository.GetAllByUser(userId);
    }

    public async Task<Group?> GetById(int groupId)
    {
        await CheckMembership(groupId);
        return await _groupRepository.GetById(groupId);
    }

    public async Task<IEnumerable<User>> GetGroupMembers(int groupId)
    {
        await CheckMembership(groupId);
        return await _groupRepository.GetGroupMembers(groupId);
    }

    public async Task Update(GroupUpdateRequest request, int groupId)
    {
        await CheckOwnership(groupId);
        await _groupRepository.Update(request, groupId);
    }

    public async Task CheckOwnership(int groupId)
    {
        var activeUserId = _userService.GetActiveUserId();
        var group = await _groupRepository.GetById(groupId);
        var isOwner = group?.OwnerId == activeUserId; 
        if(!isOwner)
            throw new UnauthorizedAccessException("You must be the group owner in order to do this.");
    }

    public async Task CheckMembership(int groupId)
    {
        var activeUserId = _userService.GetActiveUserId();
        var group = await _groupRepository.GetGroupMembers(groupId);
        var isMember = false;
        foreach (var member in group)
            isMember = member.UserId == activeUserId;

        if(!isMember)
            throw new UnauthorizedAccessException("You must be a group member in order to do this.");
    }
}

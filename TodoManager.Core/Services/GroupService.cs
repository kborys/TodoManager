using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Groups;

namespace TodoManager.Core.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Group> Create(GroupCreateRequest request)
    {
        var newGroup = new Group(request.Name, request.OwnerId);
        newGroup.GroupId = await _groupRepository.Create(request);

        return newGroup;
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int userId)
    {
        return await _groupRepository.GetAllByUser(userId);
    }
}

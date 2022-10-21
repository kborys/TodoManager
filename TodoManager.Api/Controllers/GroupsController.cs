using Microsoft.AspNetCore.Mvc;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Models.Groups;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public GroupsController(IGroupService groupService, IUserService userService)
    {
        _groupService = groupService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<Group>> Create(GroupCreateRequest request)
    {
        var activeUserId = _userService.GetActiveUserId();
        var newGroup = await _groupService.Create(request, activeUserId);

        return CreatedAtRoute("GetGroupById", new { id = newGroup.GroupId }, newGroup);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetAll()
    {
        int activeUserId = _userService.GetActiveUserId();
        var groups = await _groupService.GetAllByUser(activeUserId);

        return Ok(groups);
    }

    [HttpGet("{id:int}", Name = "GetGroupById")]
    public async Task<ActionResult<Group?>> GetById(int id)
    {
        var activeUserId = _userService.GetActiveUserId();
        var group = await _groupService.GetById(activeUserId, id);
        if(group is null)
            return NotFound("Group with given id doesn't exist in the database or you are not a member of this group.");

        return Ok(group);
    }

    [HttpPost("/{groupId:int}/members")]
    public async Task<ActionResult> AddUser(int groupId, AddGroupMemberRequest request)
    {
        var user = await _userService.GetByUserName(request.UserName);
        if(user is null)
            return NotFound("User with such UserName doesn't exist in the database.");

        var activeUserId = _userService.GetActiveUserId();
        await _groupService.AssignUser(user.UserId, activeUserId, groupId);
        
        return Ok($"User successfully added to group.");
    }
}

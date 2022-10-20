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
        request.OwnerId = _userService.GetActiveUserId();
        var newGroup = await _groupService.Create(request);

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
        return Ok();
    }
}

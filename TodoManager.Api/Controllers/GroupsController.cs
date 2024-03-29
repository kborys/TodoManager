﻿using Microsoft.AspNetCore.Mvc;
using TodoManager.Api.Helpers;
using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Models.Groups;
using TodoManager.Application.Models.Todos;
using TodoManager.Application.Models.Users;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;
    private readonly ITodoService _todoService;
    private readonly IAuthHelper _authHelper;

    public GroupsController(IGroupService groupService, IUserService userService, ITodoService todoService, IAuthHelper authHelper)
    {
        _groupService = groupService;
        _userService = userService;
        _todoService = todoService;
        _authHelper = authHelper;
    }

    [HttpPost]
    public async Task<ActionResult<Group>> Create(GroupCreateRequest request)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var newGroup = await _groupService.Create(request, activeUserId);

        return CreatedAtRoute("GetGroupById", new { groupId = newGroup.GroupId }, newGroup);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetAll()
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var groups = await _groupService.GetAllByUser(activeUserId);

        return Ok(groups);
    }

    [HttpGet("{groupId:int}", Name = "GetGroupById")]
    public async Task<ActionResult<Group?>> GetById(int groupId)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var group = await _groupService.GetById(groupId, activeUserId);

        return group is null ? NotFound() : Ok(group);
    }

    [HttpGet("{groupId:int}/Todos")]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll(int groupId)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var todos = await _todoService.GetAllByGroup(groupId, activeUserId);

        return Ok(todos);
    }

    [HttpPost("{groupId:int}/Members")]
    public async Task<ActionResult> AddMember(int groupId, AddGroupMemberRequest request)
    {
        var user = await _userService.GetByUserName(request.UserName);
        if(user is null)
            return NotFound(new { message = "User with such UserName doesn't exist in the database." });

        var activeUserId = _authHelper.GetActiveUserId();
        await _groupService.AddMember(user.UserId, groupId, activeUserId);
        return Ok(new { message = "User successfully added to the group." });
    }

    [HttpDelete("{groupId:int}/Members/{userId:int}")]
    public async Task<ActionResult> RemoveMember(int groupId, int userId)
    {
        var user = await _userService.GetById(userId);
        if (user is null)
            return NotFound(new { message = "User with such UserId doesn't exist in the database." });

        var activeUserId = _authHelper.GetActiveUserId();
        await _groupService.RemoveMember(user.UserId, groupId, activeUserId);
        return Ok(new { message = "User successfully kicked out of the group." });
    }

    [HttpGet("{groupId:int}/Members")]
    public async Task<ActionResult<IEnumerable<User>>> GetMembers(int groupId)
    {
        var activeUserId = _authHelper.GetActiveUserId();
        var members = await _groupService.GetGroupMembers(groupId, activeUserId);
        return Ok(members);
    }
}

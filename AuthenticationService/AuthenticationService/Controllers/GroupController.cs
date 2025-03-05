using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

public class GroupController : Controller
{
    private readonly IGroupService _groupService;
    
    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }
    
    [Authorize(Roles = "Player")]
    [HttpPost("addGroup")]
    public IResult AddGroup([FromBody] Group newGroup) => 
        _groupService.AddGroupService(newGroup);

    [Authorize(Roles = "Player")]
    [HttpGet("getGroup")]
    public IResult GetGroup([FromBody] string? groupName)
    {
        if (_groupService.CheckIfGroupExists(groupName))
        {
            return Results.Json(_groupService.GetGroupByNameService(groupName));
        }
        return Results.BadRequest("Такой группы не существует");
    }

    [Authorize(Roles = "Player")]
    [HttpPost("changeGroupInfo")]
    public IResult ChangeGroupInfo([FromBody] Group newGroup)
    {
        return _groupService.ChangeGroupInfoService(newGroup);
    }
    
    [Authorize(Roles = "Player")]
    [HttpPost("deleteGroup")]
    public IResult DeleteGroup([FromBody] string? groupName)
    {
        return _groupService.DeleteGroupService(groupName);
    }
}
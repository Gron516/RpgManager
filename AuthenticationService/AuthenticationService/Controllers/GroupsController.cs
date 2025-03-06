using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[Route("groups")] 
[ApiController]
public class GroupsController : Controller
{
    private readonly IGroupsService _groupsService;
    
    public GroupsController(IGroupsService groupsService)
    {
        _groupsService = groupsService;
    }
    
    //[Authorize(Roles = "Player")]
    [HttpPost]
    public async Task<IResult> AddGroup([FromBody] Group newGroup)
    {
        try
        {
            await _groupsService.AddGroup(newGroup);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Ok("Group Add");
    }

    //[Authorize(Roles = "Player")]
    [HttpGet]
    public async Task<IResult> GetAll()
    {
        try
        {
            var result = await _groupsService.GetAllGroups();
            return Results.Json(result) ;
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("{id}")]
    public async Task<IResult> GetGroup([FromRoute] Guid id)
    {
        try
        {
            var result = await _groupsService.GetGroup(id);
            return result != null ? Results.Json(result) : Results.NotFound(id);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    //[Authorize(Roles = "Player")]
    [HttpPut]
    public async Task<IResult> ChangeGroup([FromBody] Group newGroup)
    {
        try
        {
            var result = await _groupsService.ChangeGroup(newGroup);
            return result ? Results.Ok("Group Change") : Results.NotFound(newGroup.Id);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteGroup([FromRoute] Guid id)
    {
        try
        {
            await _groupsService.DeleteGroup(id);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Ok("Group Deleted");
    }
}
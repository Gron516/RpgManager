using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[Route("person-groups")] 
[ApiController]

public class PersonGroupsController : Controller
{
    private readonly IPersonGroupsService _personGroupsService;
    
    public PersonGroupsController(IPersonGroupsService personGroupsService)
    {
        _personGroupsService = personGroupsService;
    }
    
    //[Authorize(Roles = "Player")]
    [HttpPost]
    public async Task<IResult> AddPersonToGroup([FromBody] PersonGroupModel newPersonGroupModel)
    {
        try
        {
            await _personGroupsService.AddPersonToGroup(newPersonGroupModel);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Ok("Group Add");
    }

    //[Authorize(Roles = "Player")]
    [HttpGet("by-group-id/{groupId}")]
    public async Task<IResult> GetAllByGroupId(Guid groupId)
    {
        try
        {
            var result = await _personGroupsService.GetAllByGroupId(groupId);
            return result.Length != 0 ? Results.Json(result) : Results.NotFound(groupId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("by-person-id/{personId}")]
    public async Task<IResult> GetAllByPersonId(Guid personId)
    {
        try
        {
            var result = await _personGroupsService.GetAllByPersonId(personId);
            return result.Length != 0 ? Results.Json(result) : Results.NotFound(personId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("groups/by-person-id/{personId}")]
    public async Task<IResult> GetAllGroupsByPersonId([FromRoute] Guid personId)
    {
        try
        {
            var result = await _personGroupsService.GetAllGroupsByPersonId(personId);
            return result.Length != 0 ? Results.Json(result) : Results.NotFound(personId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("persons/by-group-id/{groupId}")]
    public async Task<IResult> GetAllPersonsByGroupId([FromRoute] Guid groupId)
    {
        try
        {
            var result = await _personGroupsService.GetAllPersonsByGroupId(groupId);
            return result.Length != 0 ? Results.Json(result) : Results.NotFound(groupId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetPersonGroup([FromQuery] Guid personId, [FromQuery] Guid groupId)
    {
        try
        {
            var result = await _personGroupsService.GetPersonGroup(personId, groupId);
            return result != null ? Results.Json(result) : Results.NotFound(personId + "and" + groupId );
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    //[Authorize(Roles = "Player")]
    [HttpPut]
    public async Task<IResult> ChangePersonGroup([FromBody] PersonGroupModel personGroupModel)
    {
        try
        {
            var result = await _personGroupsService.ChangePersonGroup(personGroupModel);
            return result ? Results.Ok("Group Change") : Results.NotFound(personGroupModel);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpDelete]
    public async Task<IResult> DeletePersonGroup([FromQuery] Guid personId,[FromQuery] Guid groupId)
    {
        try
        {
            await _personGroupsService.DeletePersonGroup(personId, groupId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Ok("Group Deleted");
    }
}
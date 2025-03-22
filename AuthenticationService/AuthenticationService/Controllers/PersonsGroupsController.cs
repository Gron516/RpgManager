using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[Route("persons-groups")] 
[ApiController]

public class PersonsGroupsController : Controller
{
    private readonly IPersonsGroupsService _personsGroupsService;
    
    public PersonsGroupsController(IPersonsGroupsService personGroupsService)
    {
        _personsGroupsService = personGroupsService;
    }
    
    //[Authorize(Roles = "Player")]
    [HttpPost]
    public async Task<IResult> AddConnection([FromBody] PersonGroupModel newPersonGroupModel)
    {
        try
        {
            await _personsGroupsService.AddConnection(newPersonGroupModel);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Ok("Group Add");
    }

    //[Authorize(Roles = "Player")]
    [HttpGet("person-groups/persons/{groupId}")]
    public async Task<IResult> GetAllPersonGroupByGroupId(Guid groupId)
    {
        try
        {
            var result = await _personsGroupsService.GetAllPersonGroupByGroupId(groupId);
            return Results.Json(result) ;
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("person-groups/groups/{personId}")]
    public async Task<IResult> GetAllPersonGroupByPersonId(Guid personId)
    {
        try
        {
            var result = await _personsGroupsService.GetAllPersonGroupByPersonId(personId);
            return Results.Json(result) ;
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("person-groups/by-person-id/{personId}")]
    public async Task<IResult> GetAllGroupByPersonId([FromRoute] Guid personId)
    {
        try
        {
            var result = await _personsGroupsService.GetAllGroupByPersonId(personId);
            return result != null ? Results.Json(result) : Results.NotFound(personId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpGet("person-groups/by-group-id/{groupId}")]
    public async Task<IResult> GetAllPersonByGroupId([FromRoute] Guid groupId)
    {
        try
        {
            var result = await _personsGroupsService.GetAllPersonByGroupId(groupId);
            return result != null ? Results.Json(result) : Results.NotFound(groupId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    [HttpGet("person-groups/by-both-id/{personId}/{groupId}")]
    public async Task<IResult> GetPersonGroup([FromRoute] Guid personId, [FromRoute] Guid groupId)
    {
        try
        {
            var result = await _personsGroupsService.GetPersonGroup(personId, groupId);
            return result != null ? Results.Json(result) : Results.NotFound(personId + "and" + groupId );
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    //[Authorize(Roles = "Player")]
    [HttpPut]
    public async Task<IResult> ChangePersonGroup([FromBody] PersonGroupModel newPersonGroupModel, Guid personId,Guid groupId)
    {
        try
        {
            var result = await _personsGroupsService.ChangePersonGroup(newPersonGroupModel,personId, groupId);
            return result ? Results.Ok("Group Change") : Results.NotFound(personId + "and" + groupId );
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //[Authorize(Roles = "Player")]
    [HttpDelete("{personId}/{groupId}")]
    public async Task<IResult> DeleteGroup([FromRoute] Guid personId,Guid groupId)
    {
        try
        {
            await _personsGroupsService.DeleteConnection(personId, groupId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Ok("Group Deleted");
    }
}
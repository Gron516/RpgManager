using AuthenticationService.Entities;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories;

public class PersonsGroupsRepository : IPersonsGroupsRepository
{
    private readonly ApplicationContext _context;
    
    public PersonsGroupsRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task AddConnection(PersonGroupEntity personGroup)
    {
        personGroup.JoinedAt = DateTime.UtcNow;
        await _context.PersonGroups.AddAsync(personGroup);
        await _context.SaveChangesAsync();
    }

    public async Task<PersonGroupEntity?[]> GetAllPersonGroupByGroupId(Guid groupId) => 
        await _context.PersonGroups.Where(g => g.GroupId == groupId).ToArrayAsync();
    
    public async Task<PersonGroupEntity?[]> GetAllPersonGroupByPersonId(Guid personId) => 
        await _context.PersonGroups.Where(g => g.PersonId == personId).ToArrayAsync();
    
    public async Task<PersonGroupEntity?> GetPersonGroup(Guid personId, Guid groupId) => 
        await _context.PersonGroups.FirstOrDefaultAsync(g => g.PersonId == personId && g.GroupId == groupId);

    public async Task Change(PersonGroupEntity newPersonGroup , PersonGroupEntity oldPersonGroup)
    {
        oldPersonGroup.PersonId = newPersonGroup.PersonId; //?? newPersonGroup.PersonId;
        oldPersonGroup.GroupId = newPersonGroup.GroupId; //?? newPersonGroup.GroupId;
        oldPersonGroup.GroupRole = newPersonGroup.GroupRole; //?? newPersonGroup.GroupRole;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConnection(PersonGroupEntity personGroup)
    {
        _context.PersonGroups.Remove(personGroup);
        await _context.SaveChangesAsync();
    }
    
    public async Task<PersonEntity?[]> GetAllPersonByGroupId(Guid groupId)
    {
        return await _context.PersonGroups.Where(pg => pg.GroupId == groupId).Select(pg => pg.Person).ToArrayAsync();
    }
    public async Task<GroupEntity?[]> GetAllGroupByPersonId(Guid personId)
    {
        return await _context.PersonGroups.Where(pg => pg.PersonId == personId).Select(pg => pg.Group).ToArrayAsync();
    }
}
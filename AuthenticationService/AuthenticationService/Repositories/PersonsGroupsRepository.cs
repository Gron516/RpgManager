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
    
    public async Task AddConnection(PersonGroup personGroup)
    {
        personGroup.JoinedAt = DateTime.UtcNow;
        await _context.PersonGroups.AddAsync(personGroup);
        await _context.SaveChangesAsync();
    }

    public async Task<PersonGroup?[]> GetAllPersonGroupByGroupId(Guid groupId) => 
        await _context.PersonGroups.Where(g => g.GroupId == groupId).ToArrayAsync();
    
    public async Task<PersonGroup?[]> GetAllPersonGroupByPersonId(Guid personId) => 
        await _context.PersonGroups.Where(g => g.PersonId == personId).ToArrayAsync();
    
    public async Task<PersonGroup?> GetPersonGroup(Guid personId, Guid groupId) => 
        await _context.PersonGroups.FirstOrDefaultAsync(g => g.PersonId == personId && g.GroupId == groupId);

    public async Task Change(PersonGroup newPersonGroup , PersonGroup oldPersonGroup)
    {
        oldPersonGroup.PersonId = newPersonGroup.PersonId; //?? newPersonGroup.PersonId;
        oldPersonGroup.GroupId = newPersonGroup.GroupId; //?? newPersonGroup.GroupId;
        oldPersonGroup.GroupRole = newPersonGroup.GroupRole; //?? newPersonGroup.GroupRole;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConnection(PersonGroup personGroup)
    {
        _context.PersonGroups.Remove(personGroup);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Person?[]> GetAllPersonByGroupId(Guid groupId)
    {
        return await _context.PersonGroups.Where(pg => pg.GroupId == groupId).Select(pg => pg.Person).ToArrayAsync();
    }
    public async Task<Group?[]> GetAllGroupByPersonId(Guid personId)
    {
        return await _context.PersonGroups.Where(pg => pg.PersonId == personId).Select(pg => pg.Group).ToArrayAsync();
    }
}
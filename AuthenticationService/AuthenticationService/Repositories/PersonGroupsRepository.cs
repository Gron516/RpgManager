using AuthenticationService.Entities;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories;

public class PersonGroupsRepository : IPersonGroupsRepository
{
    private readonly ApplicationContext _context;
    
    public PersonGroupsRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task Add(PersonGroupEntity personGroup)
    {
        personGroup.JoinedAt = DateTime.UtcNow;
        await _context.PersonGroups.AddAsync(personGroup);
        await _context.SaveChangesAsync();
    }

    public async Task<PersonGroupEntity?[]> GetAllByGroupId(Guid groupId) => 
        await _context.PersonGroups.Where(g => g.GroupId == groupId).ToArrayAsync();
    
    public async Task<PersonGroupEntity?[]> GetAllByPersonId(Guid personId) => 
        await _context.PersonGroups.Where(g => g.PersonId == personId).ToArrayAsync();
    
    public async Task<PersonGroupEntity?> Get(Guid personId, Guid groupId) => 
        await _context.PersonGroups.FirstOrDefaultAsync(g => g.PersonId == personId && g.GroupId == groupId);
    
    public async Task Change(PersonGroupEntity personGroup)
    {
        _context.PersonGroups.Update(personGroup);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(PersonGroupEntity personGroup)
    {
        _context.PersonGroups.Remove(personGroup);
        await _context.SaveChangesAsync();
    }
    
    public async Task<PersonEntity?[]> GetAllPersonsByGroupId(Guid groupId) => 
        await _context.PersonGroups.Where(pg => pg.GroupId == groupId).Select(pg => pg.Person).ToArrayAsync();

    public async Task<GroupEntity?[]> GetAllGroupsByPersonId(Guid personId) => 
        await _context.PersonGroups.Where(pg => pg.PersonId == personId).Select(pg => pg.Group).ToArrayAsync();
}
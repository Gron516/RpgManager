using AuthenticationService.Entities;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories;

public class GroupsRepository : IGroupsRepository
{
    private readonly ApplicationContext _context;
    
    public GroupsRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task Add(GroupEntity group)
    {
        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();
    }
    public async Task<GroupEntity?> Get(Guid id) => 
        await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

    public async Task Change(GroupEntity newGroup, GroupEntity oldGroup)
    {
        oldGroup.Name = newGroup.Name ?? oldGroup.Name;
        oldGroup.System = newGroup.System ?? oldGroup.System;
        oldGroup.Description = newGroup.Description ?? oldGroup.Description;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(GroupEntity group)
    {
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }
    
    public async Task<GroupEntity[]?> GetAll()
    {
        return await _context.Groups.ToArrayAsync();
    }
}
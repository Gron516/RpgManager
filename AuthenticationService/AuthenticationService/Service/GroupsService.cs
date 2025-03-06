using AuthenticationService.Models;
using AuthenticationService.Repositories;

namespace AuthenticationService.Service;

public class GroupsService : IGroupsService
{
    private readonly IGroupsRepository _groupsRepository;
    
    public GroupsService(IGroupsRepository groupsRepository)
    {
        _groupsRepository = groupsRepository;
    }

    public async Task AddGroup(Group group)
    {
        await _groupsRepository.Add(group);
    }

    public async Task<bool> ChangeGroup(Group group)
    {
        var foundGroup = await _groupsRepository.Get(group.Id);
        if (foundGroup == null)
            return false;
        
        await _groupsRepository.Change(group, foundGroup);
        return true;
    }

    public async Task DeleteGroup(Guid id)
    {
        var foundGroup = await _groupsRepository.Get(id);
        if (foundGroup == null)
            return;

        await _groupsRepository.Delete(foundGroup);
    }
    
    public async Task<Group?> GetGroup(Guid id) => 
        await _groupsRepository.Get(id);
    
    public async Task<Group[]?> GetAllGroups() => 
        await _groupsRepository.GetAll();
}
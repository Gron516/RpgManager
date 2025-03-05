using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IGroupsService
{
    public Task AddGroup(Group group);
    public Task<bool> ChangeGroup(Group group);
    public Task DeleteGroup(int id);
    Task<Group?> GetGroup(int id);
    Task<Group[]?> GetAllGroups();
}
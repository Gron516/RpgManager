using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IGroupsService
{
    public Task AddGroup(Group group);
    public Task<bool> ChangeGroup(Group group);
    public Task DeleteGroup(Guid id);
    Task<Group?> GetGroup(Guid id);
    Task<Group[]?> GetAllGroups();
}
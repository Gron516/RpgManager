using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IGroupsService
{
    public Task AddGroup(GroupModel groupModel);
    public Task<bool> ChangeGroup(GroupModel groupModel,Guid id);
    public Task DeleteGroup(Guid id);
    Task<GroupEntity?> GetGroup(Guid id);
    Task<GroupEntity[]?> GetAllGroups();
}
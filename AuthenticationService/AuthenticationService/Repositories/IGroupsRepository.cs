using AuthenticationService.Models;

namespace AuthenticationService.Repositories;

public interface IGroupsRepository
{
    Task Add(Group group);
    Task<Group?> Get(int id);
    Task<Group[]?> GetAll();
    Task Change(Group newGroup, Group oldGroup);
    Task Delete(Group group);
}
using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Repositories;

public interface IGroupsRepository
{
    Task Add(GroupEntity group);
    Task<GroupEntity?> Get(Guid id);
    Task<GroupEntity[]?> GetAll();
    Task Change(GroupEntity newGroup, GroupEntity oldGroup);
    Task Delete(GroupEntity group);
}
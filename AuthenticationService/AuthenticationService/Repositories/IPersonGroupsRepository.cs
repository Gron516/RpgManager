using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Repositories;

public interface IPersonGroupsRepository
{
    Task Add(PersonGroupEntity personGroup);
    Task<PersonGroupEntity?[]> GetAllByGroupId(Guid groupId);
    Task<PersonGroupEntity?[]> GetAllByPersonId(Guid personId);
    Task<PersonGroupEntity?> Get(Guid personId, Guid groupId);
    Task<PersonEntity?[]> GetAllPersonsByGroupId(Guid groupId);
    Task<GroupEntity?[]> GetAllGroupsByPersonId(Guid personId);
    Task Change(PersonGroupEntity personGroup);
    Task Delete(PersonGroupEntity personGroup);
}
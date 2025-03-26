using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Repositories;

public interface IPersonGroupsRepository
{
    Task Add(PersonGroupEntity personGroup);
    Task<PersonGroupEntity?[]> GetAllByGroupId(Guid groupId);
    Task<PersonGroupEntity?[]> GetAllPersonGroupByPersonId(Guid personId);
    Task<PersonGroupEntity?> GetPersonGroup(Guid personId, Guid groupId);
    Task<PersonEntity?[]> GetAllPersonByGroupId(Guid groupId);
    Task<GroupEntity?[]> GetAllGroupByPersonId(Guid personId);
    Task Change(PersonGroupEntity oldPersonGroup, PersonGroupEntity newPersonGroup);
    Task DeleteConnection(PersonGroupEntity personGroup);
}
using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Repositories;

public interface IPersonsGroupsRepository
{
    Task AddConnection(PersonGroupEntity personGroup);
    Task<PersonGroupEntity?[]> GetAllPersonGroupByGroupId(Guid groupId);
    Task<PersonGroupEntity?[]> GetAllPersonGroupByPersonId(Guid personId);
    Task<PersonGroupEntity?> GetPersonGroup(Guid personId, Guid groupId);
    Task<PersonEntity?[]> GetAllPersonByGroupId(Guid groupId);
    Task<GroupEntity?[]> GetAllGroupByPersonId(Guid personId);
    Task Change(PersonGroupEntity oldPersonGroup, PersonGroupEntity newPersonGroup);
    Task DeleteConnection(PersonGroupEntity personGroup);
}
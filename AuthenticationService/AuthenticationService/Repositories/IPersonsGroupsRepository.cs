using AuthenticationService.Models;

namespace AuthenticationService.Repositories;

public interface IPersonsGroupsRepository
{
    Task AddConnection(PersonGroup personGroup);
    Task<PersonGroup?[]> GetAllPersonGroupByGroupId(Guid groupId);
    Task<PersonGroup?[]> GetAllPersonGroupByPersonId(Guid personId);
    Task<PersonGroup?> GetPersonGroup(Guid personId, Guid groupId);
    Task<Person?[]> GetAllPersonByGroupId(Guid groupId);
    Task<Group?[]> GetAllGroupByPersonId(Guid personId);
    Task Change(PersonGroup oldPersonGroup, PersonGroup newPersonGroup);
    Task DeleteConnection(PersonGroup personGroup);
}
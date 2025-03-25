using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IPersonsGroupsService
{
    public Task AddConnection(PersonGroupModel personGroupModel);
    public Task<bool> ChangePersonGroup(PersonGroupModel groupModel,Guid personId, Guid groupId);
    public Task DeleteConnection(Guid personId, Guid groupId);
    Task<PersonGroup?[]> GetAllPersonGroupByGroupId(Guid groupId);
    Task<PersonGroup?[]> GetAllPersonGroupByPersonId(Guid personId);
    Task<PersonGroup?> GetPersonGroup(Guid personId, Guid groupId);
    Task<Person?[]> GetAllPersonByGroupId(Guid groupId);
    Task<Group?[]> GetAllGroupByPersonId(Guid personId);
    
}
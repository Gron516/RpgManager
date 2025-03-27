using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IPersonGroupsService
{
    public Task AddPersonToGroup(PersonGroupModel personGroupModel);
    public Task<bool> ChangePersonGroup(PersonGroupModel personGroupModel);
    public Task DeletePersonGroup(Guid personId, Guid groupId);
    Task<PersonGroupModel?[]> GetAllByGroupId(Guid groupId);
    Task<PersonGroupModel?[]> GetAllByPersonId(Guid personId);
    Task<PersonGroupModel?> GetPersonGroup(Guid personId, Guid groupId);
    Task<PersonModel?[]> GetAllPersonsByGroupId(Guid groupId);
    Task<GroupModel?[]> GetAllGroupsByPersonId(Guid personId);
    
}
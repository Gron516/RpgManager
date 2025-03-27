using AuthenticationService.Entities;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AutoMapper;

namespace AuthenticationService.Service;

public class PersonGroupsService : IPersonGroupsService
{
    private readonly IPersonGroupsRepository _personGroupsRepository;
    private readonly IMapper _mapper;
    
    public PersonGroupsService(IPersonGroupsRepository personGroupsRepository,IMapper mapper)
    {
        _personGroupsRepository = personGroupsRepository;
        _mapper = mapper;
    }

    public async Task AddPersonToGroup(PersonGroupModel personGroupModel)
    {
        await _personGroupsRepository.Add(ConvertModel(personGroupModel));
    }

    public async Task<bool> ChangePersonGroup(PersonGroupModel personGroupModel)
    {
        var foundGroup = await _personGroupsRepository.Get(personGroupModel.PersonId, personGroupModel.GroupId);
        if (foundGroup == null)
            return false;
        
        foundGroup.GroupRole = personGroupModel.GroupRole;
        foundGroup.JoinedAt = personGroupModel.JoinedAt;
        await _personGroupsRepository.Change(foundGroup);
        return true;
    }

    public async Task DeletePersonGroup(Guid personId, Guid groupId)
    {
        var foundGroup = await _personGroupsRepository.Get(personId, groupId);
        if (foundGroup == null)
            return;

        await _personGroupsRepository.Delete(foundGroup);
    }
    
    public async Task<PersonGroupModel?[]> GetAllByGroupId(Guid groupId) => 
        ConvertEntities(await _personGroupsRepository.GetAllByGroupId(groupId));
    
    public async Task<PersonGroupModel?[]> GetAllByPersonId(Guid personId) => 
        ConvertEntities(await _personGroupsRepository.GetAllByPersonId(personId));
    
    public async Task<PersonGroupModel?> GetPersonGroup(Guid personId, Guid groupId) => 
        ConvertEntity(await _personGroupsRepository.Get(personId, groupId));
    
    public async Task<PersonModel?[]> GetAllPersonsByGroupId(Guid groupId) => 
        ConvertEntities(await _personGroupsRepository.GetAllPersonsByGroupId(groupId));
    
    public async Task<GroupModel?[]> GetAllGroupsByPersonId(Guid personId) => 
        ConvertEntities(await _personGroupsRepository.GetAllGroupsByPersonId(personId));
    
    private PersonGroupEntity ConvertModel(PersonGroupModel model) => 
        _mapper.Map<PersonGroupEntity>(model);
    
    private PersonGroupModel?[] ConvertEntities(PersonGroupEntity?[] entities) => 
        _mapper.Map<PersonGroupModel?[]>(entities);
    private PersonGroupModel? ConvertEntity(PersonGroupEntity? entity) => 
        _mapper.Map<PersonGroupModel?>(entity);
    private PersonModel?[] ConvertEntities(PersonEntity?[] entities) => 
        _mapper.Map<PersonModel?[]>(entities);
    private GroupModel?[] ConvertEntities(GroupEntity?[] entities) => 
        _mapper.Map<GroupModel?[]>(entities);
}
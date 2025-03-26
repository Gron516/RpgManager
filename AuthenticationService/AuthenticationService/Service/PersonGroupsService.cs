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

    public async Task<bool> ChangePersonGroup(PersonGroupModel personGroupModel,Guid personId, Guid groupId)
    {
        var foundGroup = await _personGroupsRepository.GetPersonGroup(personId, groupId);
        if (foundGroup == null)
            return false;
        
        await _personGroupsRepository.Change(ConvertModel(personGroupModel), foundGroup);
        return true;
    }

    public async Task DeleteConnection(Guid personId, Guid groupId)
    {
        var foundGroup = await _personGroupsRepository.GetPersonGroup(personId, groupId);
        if (foundGroup == null)
            return;

        await _personGroupsRepository.DeleteConnection(foundGroup);
    }
    
    public async Task<PersonGroupModel?[]> GetAllByGroupId(Guid groupId) => 
        ConvertEntities(await _personGroupsRepository.GetAllByGroupId(groupId));
    
    public async Task<PersonGroupEntity?[]> GetAllPersonGroupByPersonId(Guid personId) => 
        await _personGroupsRepository.GetAllPersonGroupByPersonId(personId);
    
    public async Task<PersonGroupEntity?> GetPersonGroup(Guid personId, Guid groupId) => 
        await _personGroupsRepository.GetPersonGroup(personId, groupId);
    
    public async Task<PersonEntity?[]> GetAllPersonByGroupId(Guid groupId) => 
        await _personGroupsRepository.GetAllPersonByGroupId(groupId);
    
    public async Task<GroupEntity?[]> GetAllGroupByPersonId(Guid personId) => 
        await _personGroupsRepository.GetAllGroupByPersonId(personId);
    
    private PersonGroupEntity ConvertModel(PersonGroupModel model) => 
        _mapper.Map<PersonGroupEntity>(model);
    
    private PersonGroupModel?[] ConvertEntities(PersonGroupEntity?[] entities) => 
        _mapper.Map<PersonGroupModel?[]>(entities);
}
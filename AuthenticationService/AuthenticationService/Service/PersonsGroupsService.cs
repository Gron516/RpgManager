using AuthenticationService.Entities;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AutoMapper;

namespace AuthenticationService.Service;

public class PersonsGroupsService : IPersonsGroupsService
{
    private readonly IPersonsGroupsRepository _personsGroupsRepository;
    private readonly IMapper _mapper;
    
    public PersonsGroupsService(IPersonsGroupsRepository personsGroupsRepository,IMapper mapper)
    {
        _personsGroupsRepository = personsGroupsRepository;
        _mapper = mapper;
    }

    public async Task AddConnection(PersonGroupModel personGroupModel)
    {
        await _personsGroupsRepository.AddConnection(ConvertModel(personGroupModel));
    }

    public async Task<bool> ChangePersonGroup(PersonGroupModel personGroupModel,Guid personId, Guid groupId)
    {
        var foundGroup = await _personsGroupsRepository.GetPersonGroup(personId, groupId);
        if (foundGroup == null)
            return false;
        
        await _personsGroupsRepository.Change(ConvertModel(personGroupModel), foundGroup);
        return true;
    }

    public async Task DeleteConnection(Guid personId, Guid groupId)
    {
        var foundGroup = await _personsGroupsRepository.GetPersonGroup(personId, groupId);
        if (foundGroup == null)
            return;

        await _personsGroupsRepository.DeleteConnection(foundGroup);
    }
    
    public async Task<PersonGroupEntity?[]> GetAllPersonGroupByGroupId(Guid groupId) => 
        await _personsGroupsRepository.GetAllPersonGroupByGroupId(groupId);
    
    public async Task<PersonGroupEntity?[]> GetAllPersonGroupByPersonId(Guid personId) => 
        await _personsGroupsRepository.GetAllPersonGroupByPersonId(personId);
    
    public async Task<PersonGroupEntity?> GetPersonGroup(Guid personId, Guid groupId) => 
        await _personsGroupsRepository.GetPersonGroup(personId, groupId);
    
    public async Task<PersonEntity?[]> GetAllPersonByGroupId(Guid groupId) => 
        await _personsGroupsRepository.GetAllPersonByGroupId(groupId);
    
    public async Task<GroupEntity?[]> GetAllGroupByPersonId(Guid personId) => 
        await _personsGroupsRepository.GetAllGroupByPersonId(personId);
    
    private PersonGroupEntity ConvertModel(PersonGroupModel model) => 
        _mapper.Map<PersonGroupEntity>(model);
}
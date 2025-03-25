using AuthenticationService.Entities;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AutoMapper;

namespace AuthenticationService.Service;

public class GroupsService : IGroupsService
{
    private readonly IGroupsRepository _groupsRepository;
    private readonly IMapper _mapper;
    
    public GroupsService(IGroupsRepository groupsRepository,IMapper mapper)
    {
        _groupsRepository = groupsRepository;
        _mapper = mapper;
    }

    public async Task AddGroup(GroupModel groupModel)
    {
        await _groupsRepository.Add(ConvertModel(groupModel));
    }

    public async Task<bool> ChangeGroup(GroupModel groupModel,Guid id)
    {
        var foundGroup = await _groupsRepository.Get(id);
        if (foundGroup == null)
            return false;
        
        await _groupsRepository.Change(ConvertModel(groupModel), foundGroup);
        return true;
    }

    public async Task DeleteGroup(Guid id)
    {
        var foundGroup = await _groupsRepository.Get(id);
        if (foundGroup == null)
            return;

        await _groupsRepository.Delete(foundGroup);
    }
    
    public async Task<GroupEntity?> GetGroup(Guid id) => 
        await _groupsRepository.Get(id);
    
    public async Task<GroupEntity[]?> GetAllGroups() => 
        await _groupsRepository.GetAll();
    private GroupEntity ConvertModel(GroupModel model) => 
        _mapper.Map<GroupEntity>(model);
}
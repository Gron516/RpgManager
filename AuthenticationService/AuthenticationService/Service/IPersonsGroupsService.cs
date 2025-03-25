﻿using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IPersonsGroupsService
{
    public Task AddConnection(PersonGroupModel personGroupModel);
    public Task<bool> ChangePersonGroup(PersonGroupModel groupModel,Guid personId, Guid groupId);
    public Task DeleteConnection(Guid personId, Guid groupId);
    Task<PersonGroupEntity?[]> GetAllPersonGroupByGroupId(Guid groupId);
    Task<PersonGroupEntity?[]> GetAllPersonGroupByPersonId(Guid personId);
    Task<PersonGroupEntity?> GetPersonGroup(Guid personId, Guid groupId);
    Task<PersonEntity?[]> GetAllPersonByGroupId(Guid groupId);
    Task<GroupEntity?[]> GetAllGroupByPersonId(Guid personId);
    
}
using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IGroupService
{
    public IResult AddGroupService(Group group);
    public Group? GetGroupByNameService(string? groupName);
    public  IResult ChangeGroupInfoService(Group group);
    public  IResult DeleteGroupService(string? groupName);
    public bool CheckIfGroupExists(string? name);
}
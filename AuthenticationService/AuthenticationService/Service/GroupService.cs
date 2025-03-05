using AuthenticationService.Models;

namespace AuthenticationService.Service;

public class GroupService : IGroupService
{
    private readonly ApplicationContext _context;
    
    public GroupService(ApplicationContext context)
    {
        _context = context;
    }

    public IResult AddGroupService(Group group)
    {
        if (CheckIfGroupExists(group.Name))
        {
            return Results.BadRequest("Такая группа уже существует");
        }
        AddGrouptoDbService(group);
        return Results.Ok("Group created");
    }
    private void AddGrouptoDbService(Group group)
    {
        _context.Groups.Add(group);
        _context.SaveChanges();
    }

    public Group? GetGroupByNameService(string? groupName)
    {
        return _context.Groups.FirstOrDefault(g => g.Name == groupName);;
    }
        

    public IResult ChangeGroupInfoService(Group group)
    {
        if (!CheckIfGroupExists(group.Name))
        {
            return Results.BadRequest("Такой группы не существует");
        }
        Group? groupFind = GetGroupByNameService(group.Name);
        if (groupFind != null)
        {
            if (group.System != null)
                groupFind.System = group.System;
            
            if (group.Description != null)
                groupFind.Description = group.Description;
        }
        _context.SaveChanges();
        return Results.Ok("Group Change");
    }

    public IResult DeleteGroupService(string? groupName)
    {
        if (CheckIfGroupExists(groupName))
        {
            if (GetGroupByNameService(groupName) != null)
            {
                _context.Groups.Remove(GetGroupByNameService(groupName));
                _context.SaveChanges();
                return Results.Ok("Group Delete");
            }
        }
        return Results.BadRequest("Такой группы не существует");
    }
    public bool CheckIfGroupExists(string? name) => 
            _context.Groups.Any(g => g.Name == name);

}
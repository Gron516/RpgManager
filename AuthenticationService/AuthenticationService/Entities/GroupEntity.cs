namespace AuthenticationService.Entities;

public class GroupEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? System { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public ICollection<PersonGroupEntity> PersonGroups { get; set; } = new List<PersonGroupEntity>();
}
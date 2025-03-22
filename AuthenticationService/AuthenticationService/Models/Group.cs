using AuthenticationService.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? System { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();
}
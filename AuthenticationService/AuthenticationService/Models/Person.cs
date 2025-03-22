using AuthenticationService.Models;

public record class Person
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; }
    public string? Image { get; set; }

    public ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();
}
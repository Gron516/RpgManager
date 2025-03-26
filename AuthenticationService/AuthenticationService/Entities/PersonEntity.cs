namespace AuthenticationService.Entities;

public record class PersonEntity
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; }
    public string? Image { get; set; }

    public ICollection<PersonGroupEntity> PersonGroups { get; set; } = new List<PersonGroupEntity>();
}
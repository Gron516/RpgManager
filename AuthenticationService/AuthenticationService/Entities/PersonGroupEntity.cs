using AuthenticationService.Models;

namespace AuthenticationService.Entities;

public class PersonGroupEntity
{
    public Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    public Guid GroupId { get; set; }
    public GroupEntity? Group { get; set; }

    public DateTime JoinedAt { get; set; }

    public GroupRole GroupRole{ get; set; }
}
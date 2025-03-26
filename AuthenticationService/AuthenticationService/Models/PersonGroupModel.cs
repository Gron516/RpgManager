namespace AuthenticationService.Models;

public class PersonGroupModel
{
    public Guid PersonId { get; set; }

    public Guid GroupId { get; set; }

    public DateTime JoinedAt { get; set; }

    public GroupRole GroupRole{ get; set; }
}
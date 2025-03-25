using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Models;

public class PersonGroup
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid GroupId { get; set; }
    public Group? Group { get; set; }

    public DateTime JoinedAt { get; set; }

    public GroupRole GroupRole{ get; set; }
}
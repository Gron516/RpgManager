namespace AuthenticationService.Models;

public class PersonModel
{
    public string? Email { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Country { get; set; }
}
namespace AuthenticationService.Service;

public interface IAuthService
{
    public Person? FindPerson(Person loginData);
    public string? CreateToken(Person person);
}
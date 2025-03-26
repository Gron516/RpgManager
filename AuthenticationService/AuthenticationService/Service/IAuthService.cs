using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IAuthService
{
    public Person? FindPerson(PersonModel loginData);
    public string? CreateToken(Person person);
    public IResult AddPerson(PersonModel loginData);
}
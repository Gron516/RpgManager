using AuthenticationService.Entities;
using AuthenticationService.Models;

namespace AuthenticationService.Service;

public interface IAuthService
{
    public PersonEntity? FindPerson(PersonModel loginData);
    public string? CreateToken(PersonEntity person);
    public IResult AddPerson(PersonModel loginData);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationService.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Service;

public class AuthService : IAuthService
{
    private readonly ApplicationContext _context;

    public AuthService(ApplicationContext context)
    {
        _context = context;
    }
    
    public Person? FindPerson(Person loginData)
    {
        // находим пользователя 
        return FindInDb(new() { Email = loginData.Email, Password = loginData.Password });
    }
    
    public string? CreateToken(Person person)
    {
    var claims = new List<Claim> {new Claim(ClaimTypes.Name, person.Email) };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }

    public IResult AddPerson(Person loginData)
    {
        var user = new Person
        {
            Email = loginData.Email
        };
        var person = FindLoginInDb(user);
        // если пользователь не найден, отправляем статусный код 401
        if(person is null) 
        {
            AddToDb(loginData);
            return Results.Text("Login Successful");
        }
        else
        {
            return Results.BadRequest();
        }
    }
    
    private void AddToDb(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
    }
    
    private Person? FindInDb(Person person) => 
        _context.Persons.FirstOrDefault(p => p.Email == person.Email && p.Password == person.Password);
    private Person? FindLoginInDb(Person person) => 
        _context.Persons.FirstOrDefault(p => p.Email == person.Email);
}
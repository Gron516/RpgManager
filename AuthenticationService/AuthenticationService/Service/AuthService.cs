using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthenticationService.Configurations;
using Microsoft.EntityFrameworkCore;
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
        return FindInDb(new() { Email = loginData.Email, Password = GetHash(loginData.Password) });
    }
    
    public string? CreateToken(Person person)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
        };

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

    public IResult AddPerson(Person registrationData)
    {
        if(CheckIfUserExists(registrationData.Email))
        {
            // если пользователь найден, отправляем статусный код 400
            return Results.BadRequest("Такой пользователь уже существует");
        }
        AddToDb(registrationData);
        return Results.Ok("Login Successful");
    }
    
    private void AddToDb(Person person)
    {
        person.Password = GetHash(person.Password);
        person.Role = "Player";
        _context.Persons.Add(person);
        _context.SaveChanges();
    }
    
    private Person? FindInDb(Person person) => 
        _context.Persons.FirstOrDefault(p => p.Email == person.Email && p.Password == person.Password);
    
    private bool CheckIfUserExists(string? login) => 
        _context.Persons.Any(p => p.Email == login);
    
    private string GetHash(string input) => 
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
}
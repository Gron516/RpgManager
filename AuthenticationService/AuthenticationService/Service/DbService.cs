using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationService.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Service;

public class DbService
{
    public static Person AuthServiceFindPerson([FromBody] Person loginData)
    {
        // находим пользователя 
        var person = FindinDb(new() { Email = loginData.Email, Password = loginData.Password });
        return person;
    }
    
    public static  string? AuthServiceCreateToken([FromBody] Person person)
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
    
    
    public static void AddtoDb(Person person)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }
        
    }
    
    public static Person FindinDb(Person person)
    {

        using (ApplicationContext db = new ApplicationContext())
        {
            Person wantedUseer = person;
            var user = db.Persons.FirstOrDefault(p => p.Email == wantedUseer.Email && p.Password == wantedUseer.Password);
            return user;
        }
        
    }
}
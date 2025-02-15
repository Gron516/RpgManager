using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationService.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Controllers;

public class AuthController : Controller
{
    [HttpPost("login")]
    public IResult Login([FromBody]Person loginData)
    {
        // находим пользователя 
        var person = FindinDb(new() { Email = loginData.Email, Password = loginData.Password });
        // если пользователь не найден, отправляем статусный код 401
        if(person is null) return Results.Unauthorized();
     
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, person.Email) };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        // формируем ответ
        var response = new
        {
            access_token = encodedJwt,
            username = person.Email
        };
 
        return Results.Json(response);
    }

    [Authorize]
    [HttpGet("data")]
    public IResult Data()
    {
        var response = new
        {
            message = "Hello World!"
        };
 
        return Results.Json(response);
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


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthenticationService.Configurations;
using AuthenticationService.Entities;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace AuthenticationService.Service;

public class AuthService : IAuthService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public AuthService(ApplicationContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public PersonEntity? FindPerson(PersonModel loginDataModel)
    {
        // находим пользователя 
        return FindInDb(ConvertModel(loginDataModel));
    }
    
    public string? CreateToken(PersonEntity person)
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

    public IResult AddPerson(PersonModel registrationData)
    {
        if(CheckIfUserExists(registrationData.Email))
        {
            // если пользователь найден, отправляем статусный код 400
            return Results.BadRequest("Такой пользователь уже существует");
        }
        AddToDb(ConvertModel(registrationData));
        return Results.Ok("Login Successful");
    }
    
    private void AddToDb(PersonEntity person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
    }
    
    private PersonEntity? FindInDb(PersonEntity? person) => 
        _context.Persons.FirstOrDefault(p => p.Email == person.Email && p.Password == person.Password);
    
    private bool CheckIfUserExists(string? login) => 
        _context.Persons.Any(p => p.Email == login);

    private PersonEntity? ConvertModel(PersonModel model)
    {
        model.Role = "Player";
        return _mapper.Map<PersonEntity>(model);
    }
}
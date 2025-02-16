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

        var person = Service.DbService.AuthServiceFindPerson(loginData);
        // если пользователь не найден, отправляем статусный код 401
        if(person is null) return Results.Unauthorized();
        // формируем ответ
        
        var response = new
        {
            access_token =  Service.DbService.AuthServiceCreateToken(person),
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



}


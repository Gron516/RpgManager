using AuthenticationService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public IResult Login([FromBody]Person loginData)
    {

        var person = _authService.FindPerson(loginData);
        // если пользователь не найден, отправляем статусный код 401
        if(person is null) return Results.Unauthorized();
        // формируем ответ
        
        var response = new
        {
            access_token =  _authService.CreateToken(person),
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

    [HttpPost("SignUp")]
    public IResult SignUp([FromBody]Person loginData)
    {
        return _authService.AddPerson(loginData);
    }

}


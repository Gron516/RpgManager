using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationService.Configurations;
using AuthenticationService.Controllers;
using AuthenticationService.Repositories;
using AuthenticationService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IGroupsService, GroupsService>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<IGroupsRepository, GroupsRepository>();

builder.Services.AddTransient<IPersonsGroupsRepository, PersonsGroupsRepository>();
builder.Services.AddTransient<IPersonsGroupsService, PersonsGroupsService>();

builder.Services.AddTransient<ApplicationContext, ApplicationContext>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Database.Migrate(); // Накатывает миграции
}


app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;
 
public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
 
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    
    private readonly IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("AuthenticationServiceDb"));
    }
}
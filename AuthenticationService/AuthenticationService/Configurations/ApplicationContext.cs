using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<PersonGroup> PersonGroups { get; set; } = null!;


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
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("AuthenticationServiceDb"))
            .EnableSensitiveDataLogging() // Показывает значения параметров в SQL
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonGroup>()
            .HasKey(ug => new { ug.PersonId, ug.GroupId }); // Композитный ключ

        modelBuilder.Entity<PersonGroup>()
            .HasOne(ug => ug.Person)
            .WithMany(u => u.PersonGroups)
            .HasForeignKey(ug => ug.PersonId);

        modelBuilder.Entity<PersonGroup>()
            .HasOne(ug => ug.Group)
            .WithMany(g => g.PersonGroups)
            .HasForeignKey(ug => ug.GroupId);
    }
}
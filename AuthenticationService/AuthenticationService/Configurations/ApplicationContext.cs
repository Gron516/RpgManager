using AuthenticationService.Configurations;
using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<PersonEntity> Persons { get; set; } = null!;
    public DbSet<GroupEntity> Groups { get; set; } = null!;
    public DbSet<PersonGroupEntity> PersonGroups { get; set; } = null!;
    
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
        modelBuilder.ApplyConfiguration(new PersonGroupEntityConfiguration());
        modelBuilder.Entity<GroupEntity>().ToTable("Groups");
        modelBuilder.Entity<PersonEntity>().ToTable("Persons");
    }
}
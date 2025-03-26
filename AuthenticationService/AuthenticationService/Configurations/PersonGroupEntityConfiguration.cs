using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Configurations;

internal sealed class PersonGroupEntityConfiguration : IEntityTypeConfiguration<PersonGroupEntity>
{
    public void Configure(EntityTypeBuilder<PersonGroupEntity> builder)
    { 
        builder.ToTable("PersonGroups");
        builder
            .HasKey(ug => new { ug.PersonId, ug.GroupId }); // Композитный ключ

        builder
            .HasOne(ug => ug.Person)
            .WithMany(u => u.PersonGroups)
            .HasForeignKey(ug => ug.PersonId);
       
        builder
            .HasOne(ug => ug.Group)
            .WithMany(g => g.PersonGroups)
            .HasForeignKey(ug => ug.GroupId);
    }
}
using Core.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.ApplicationUserId);

        builder.Property(e => e.Login).HasMaxLength(50).IsRequired();

        builder
            .HasMany(e => e.Roles)
            .WithOne(e => e.ApplicationUser)
            .HasForeignKey(e => e.ApplicationUserId);
        
        builder.Navigation(e => e.Roles).AutoInclude();
    }
}
using Core.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserRoleTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(e => e.ApplicationUserRoleId);

        builder.Property(e => e.Name).HasMaxLength(20).IsRequired();

        builder.HasMany(e => e.Users)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.ApplicationUserRoleId);
    }
}
using Core.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserApplicationUserRoleTypeConfiguration : IEntityTypeConfiguration<ApplicationUserApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserApplicationUserRole> builder)
    {
        builder.HasKey(e => new { e.ApplicationUserRoleId, e.ApplicationUserId });

        builder.HasOne(e => e.ApplicationUser)
            .WithMany(e => e.Roles)
            .HasForeignKey(e => e.ApplicationUserId);

        builder.HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.ApplicationUserRoleId);
    }
}
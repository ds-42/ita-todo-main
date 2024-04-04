using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todos.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Todos;

public class TodoTypeConfiguration: IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(e => e.TodoId);

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

        builder.HasOne(e => e.Owner).WithMany().HasForeignKey(e => e.OwnerId);
    }
}
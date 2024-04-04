namespace Core.Application.Abstractions.Persistence;

public interface IDatabaseMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken);
    void Migrate();
    IEnumerable<string> GetPendingMigrations();
}
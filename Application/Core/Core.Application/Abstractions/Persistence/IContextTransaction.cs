namespace Core.Application.Abstractions.Persistence;

public interface IContextTransaction : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}
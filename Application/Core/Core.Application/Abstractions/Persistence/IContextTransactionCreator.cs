namespace Core.Application.Abstractions.Persistence;

public interface IContextTransactionCreator
{
    Task<IContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
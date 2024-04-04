using Core.Application.Abstractions.Persistence;

namespace Infrastructure.Persistence;

internal class ContextTransactionCreator : IContextTransactionCreator
{
    private readonly ApplicationDbContext _dbContext;

    public ContextTransactionCreator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return new ContextTransaction(await _dbContext.Database.BeginTransactionAsync(cancellationToken));
    }
}
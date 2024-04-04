using Core.Application.Abstractions.Persistence;
using MediatR;

namespace Core.Application.Behavior;

public class DatabaseTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IContextTransactionCreator _applicationContextTransactionCreator;

    public DatabaseTransactionBehavior(IContextTransactionCreator applicationContextTransactionCreator)
    {
        _applicationContextTransactionCreator = applicationContextTransactionCreator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var transaction = await _applicationContextTransactionCreator.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            throw;
        }
    }
}
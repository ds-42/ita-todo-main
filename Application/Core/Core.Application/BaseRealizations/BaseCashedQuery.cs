using Core.Application.Abstractions;
using MediatR;

namespace Core.Application.BaseRealizations;

public abstract class BaseCashedQuery<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest: IRequest<TResult>
{
    private readonly IBaseCache<TResult> Cache;

    public BaseCashedQuery(IBaseCache<TResult> cache)
    {
        Cache = cache;
    }

    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (Cache.TryGetValue(request, out TResult? result))
        {
            return result!;
        }

        result = await SentQueryAsync(request, cancellationToken);
        
        Cache.Set(request, result, 1);
        return result;
    }

    public abstract Task<TResult> SentQueryAsync(TRequest request, CancellationToken cancellationToken);
}
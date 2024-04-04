using Core.Application.Abstractions;
using MediatR;

namespace Core.Application.BaseRealizations;

public abstract class BaseCashedForUserQuery<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest: IRequest<TResult>
{
    private readonly IBaseCache<TResult> _cache;

    private readonly Guid _applicationUserId;

    public BaseCashedForUserQuery(IBaseCache<TResult> cache, Guid applicationUserId)
    {
        _cache = cache;
        _applicationUserId = applicationUserId;
    }

    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request, _applicationUserId.ToString(), out TResult? result))
        {
            return result!;
        }

        result = await SentQueryAsync(request, cancellationToken);
        
        _cache.Set(request, _applicationUserId.ToString(), result, 1);
        return result;
    }

    public abstract Task<TResult> SentQueryAsync(TRequest request, CancellationToken cancellationToken);
}
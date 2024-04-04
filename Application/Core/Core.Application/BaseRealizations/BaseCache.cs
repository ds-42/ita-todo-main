using System.Text.Json;
using Core.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Application.BaseRealizations;

public abstract class BaseCache<TItem> : IBaseCache<TItem>
{
    protected MemoryCache Cache  = new(
        new MemoryCacheOptions
        {
            SizeLimit = 1024
        });
    
    protected virtual int AbsoluteExpiration => 10;

    protected virtual int SlidingExpiration => 5;

    private string CreateCacheKey<TRequest>(TRequest request)
    {
        return JsonSerializer.Serialize(request);
    }
    
    private string CreateCacheKey<TRequest>(TRequest request, string secondKey)
    {
        return $"{JsonSerializer.Serialize(request)}_{secondKey}";
    }
    
    public void Set<TRequest>(TRequest request, string secondKey, TItem item,  int size)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(AbsoluteExpiration))
            .SetSlidingExpiration(TimeSpan.FromMinutes(SlidingExpiration))
            .SetSize(size);
        Cache.Set(CreateCacheKey(request, secondKey), item, cacheEntryOptions);
    }

    public void Set<TRequest>(TRequest request, TItem item,  int size)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(AbsoluteExpiration))
            .SetSlidingExpiration(TimeSpan.FromMinutes(SlidingExpiration))
            .SetSize(size);
        Cache.Set(CreateCacheKey(request), item, cacheEntryOptions);
    }

    public bool TryGetValue<TRequest>(TRequest request, out TItem? item)
    {
        return Cache.TryGetValue(CreateCacheKey(request), out item);
    }

    public bool TryGetValue<TRequest>(TRequest request, string secondKey, out TItem? item)
    {
        return Cache.TryGetValue(CreateCacheKey(request, secondKey), out item);
    }

    public void DeleteItem<TRequest>(TRequest request)
    {
        Cache.Remove(CreateCacheKey(request));
    }
    
    public void DeleteItem<TRequest>(TRequest request, string secondKey)
    {
        Cache.Remove(CreateCacheKey(request, secondKey));
    }

    public void Clear()
    {
        Cache.Clear();
    }
}
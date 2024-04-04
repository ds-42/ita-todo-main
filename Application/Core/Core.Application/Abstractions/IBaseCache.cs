namespace Core.Application.Abstractions;

public interface IBaseCache<T>
{
    void Set<TRequest>(TRequest request, string secondKey, T item, int size);
    void Set<TRequest>(TRequest request, T item,  int size);
    bool TryGetValue<TRequest>(TRequest request, out T? item);
    bool TryGetValue<TRequest>(TRequest request, string secondKey, out T? item);
    void Clear();
}
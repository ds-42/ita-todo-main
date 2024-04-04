namespace Todos.Applications.Caches;

internal class CleanTodosCacheService : ICleanTodosCacheService
{
    private readonly TodoMemoryCache _todoMemoryCache;
    
    private readonly TodosListMemoryCache _todosListMemoryCache;
    
    private readonly TodosCountMemoryCache _todosCountMemoryCache;

    public CleanTodosCacheService(
        TodoMemoryCache todoMemoryCache, 
        TodosListMemoryCache todosListMemoryCache,
        TodosCountMemoryCache todosCountMemoryCache)
    {
        _todoMemoryCache = todoMemoryCache;
        _todosListMemoryCache = todosListMemoryCache;
        _todosCountMemoryCache = todosCountMemoryCache;
    }

    public void ClearAllCaches()
    {
        _todoMemoryCache.Clear();
        _todosListMemoryCache.Clear();
        _todosCountMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _todosListMemoryCache.Clear();
        _todosCountMemoryCache.Clear();
    }
}
namespace Todos.Applications.Caches;

internal interface ICleanTodosCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}
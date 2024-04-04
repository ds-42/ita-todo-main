using System.Linq.Expressions;
using Todos.Domain;

namespace Todos.Applications.Handlers.Queries;

internal static class ListTodoWhere
{
    public static Expression<Func<Todo, bool>> WhereForClient(ListTodoFilter filter, Guid currentUserId)
    {
        var freeText = filter.FreeText?.Trim();
        return e => e.OwnerId == currentUserId &&
                    (freeText == null || e.Name.Contains(freeText)) &&
                    (filter.IsDone == null || e.IsDone == filter.IsDone);
    }

    public static Expression<Func<Todo, bool>> WhereForAdmin(ListTodoFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => (freeText == null || e.Name.Contains(freeText)) &&
                    (filter.IsDone == null || e.IsDone == filter.IsDone);
    }
}
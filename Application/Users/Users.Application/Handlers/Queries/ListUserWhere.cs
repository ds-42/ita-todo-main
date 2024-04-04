using System.Linq.Expressions;
using Core.Users.Domain;

namespace Users.Application.Handlers.Queries;

internal static class ListWhere
{
    public static Expression<Func<ApplicationUser, bool>> Where(ListUserFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.Login.Contains(freeText);
    }
}
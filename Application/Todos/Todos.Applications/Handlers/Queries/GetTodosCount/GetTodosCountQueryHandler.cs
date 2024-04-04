using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Todos.Applications.Caches;
using Todos.Domain;

namespace Todos.Applications.Handlers.Queries.GetTodosCount;

internal class GetTodosCountQueryHandler : BaseCashedForUserQuery<GetTodosCountQuery, int>
{
    private readonly IBaseReadRepository<Todo> _todos;
    
    private readonly ICurrentUserService _currentUserService;

    public GetTodosCountQueryHandler(IBaseReadRepository<Todo> todos, TodosCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _todos = todos;
        _currentUserService = currentUserService;
    }
    
    public override async Task<int> SentQueryAsync(GetTodosCountQuery request, CancellationToken cancellationToken)
    {
        return await _todos.AsAsyncRead().CountAsync(_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) ? ListTodoWhere.WhereForAdmin(request) : ListTodoWhere.WhereForClient(request, _currentUserService.CurrentUserId!.Value), cancellationToken);
    }
}
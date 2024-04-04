using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using MediatR;
using Todos.Applications.Caches;
using Todos.Applications.DTOs;
using Todos.Domain;

namespace Todos.Applications.Handlers.Queries.GetTodos;

internal class GetTodosQueryHandler : BaseCashedForUserQuery<GetTodosQuery, BaseListDto<GetTodoDto>>
{
    private readonly IBaseReadRepository<Todo> _todos;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly IMapper _mapper;
    
    public GetTodosQueryHandler(IBaseReadRepository<Todo> todos, ICurrentUserService currentUserService, IMapper mapper, TodosListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _todos = todos;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetTodoDto>> SentQueryAsync(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var query = _todos.AsQueryable();

        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        var userIsAdmin = _currentUserService.UserInRole(ApplicationUserRolesEnum.Admin);
        if (userIsAdmin)
        {
            query = query.Where(ListTodoWhere.WhereForAdmin(request));
        }
        else
        {
            query = query.Where(ListTodoWhere.WhereForClient(request, _currentUserService.CurrentUserId!.Value));
        }
        
        query = query.OrderBy(e => e.TodoId);

        var items = await _todos.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _todos.AsAsyncRead().CountAsync(query, cancellationToken);

        return new BaseListDto<GetTodoDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<DTOs.GetTodoDto[]>(items)
        };
    }
}
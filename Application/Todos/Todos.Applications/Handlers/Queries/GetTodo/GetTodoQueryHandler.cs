using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using Todos.Applications.Caches;
using Todos.Applications.DTOs;
using Todos.Domain;

namespace Todos.Applications.Handlers.Queries.GetTodo;

internal class GetTodoQueryHandler : BaseCashedForUserQuery<GetTodoQuery, GetTodoDto>
{
    private readonly IBaseReadRepository<Todo> _todos;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetTodoQueryHandler(IBaseReadRepository<Todo> todos, ICurrentUserService currentUserService, IMapper mapper,
        TodoMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _todos = todos;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<GetTodoDto> SentQueryAsync(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var todo = await _todos.AsAsyncRead().SingleOrDefaultAsync(e => e.TodoId == request.TodoId, cancellationToken);
        if (todo is null)
        {
            throw new NotFoundException(request);
        }

        if (todo.OwnerId != _currentUserService.CurrentUserId &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        return _mapper.Map<GetTodoDto>(todo);
    }
}
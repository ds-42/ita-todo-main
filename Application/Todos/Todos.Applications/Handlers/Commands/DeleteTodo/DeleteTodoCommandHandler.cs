using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Todos.Applications.Caches;
using Todos.Domain;

namespace Todos.Applications.Handlers.Commands.DeleteTodo;

internal class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly IBaseWriteRepository<Todo> _todos;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly ICleanTodosCacheService _cleanTodosCacheService;

    public DeleteTodoCommandHandler(IBaseWriteRepository<Todo> todos, 
        ICurrentUserService currentUserService, ICleanTodosCacheService cleanTodosCacheService)
    {
        _todos = todos;
        _currentUserService = currentUserService;
        _cleanTodosCacheService = cleanTodosCacheService;
    }
    
    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
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

        await _todos.RemoveAsync(todo, cancellationToken);
        _cleanTodosCacheService.ClearAllCaches();
    }
}
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Todos.Applications.Caches;
using Todos.Applications.DTOs;
using Todos.Domain;

namespace Todos.Applications.Handlers.Commands.UpdateTodoIsDone;

internal class UpdateTodoIsDoneCommandHandler : IRequestHandler<UpdateTodoIsDoneCommand, GetTodoDto>
{
    private readonly IBaseWriteRepository<Todo> _todos;
    
    private readonly IMapper _mapper;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly ICleanTodosCacheService _cleanTodosCacheService;

    public UpdateTodoIsDoneCommandHandler(
        IBaseWriteRepository<Todo> todos, 
        IMapper mapper,
        ICurrentUserService currentUserService,
        ICleanTodosCacheService cleanTodosCacheService)
    {
        _todos = todos;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanTodosCacheService = cleanTodosCacheService;
    }
    public async Task<GetTodoDto> Handle(UpdateTodoIsDoneCommand request, CancellationToken cancellationToken)
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

        todo.IsDone = request.IsDone;
        await _todos.UpdateAsync(todo, cancellationToken);
        _cleanTodosCacheService.ClearAllCaches();
        return _mapper.Map<GetTodoDto>(todo);
    }
}
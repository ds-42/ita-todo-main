using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using MediatR;
using Todos.Applications.Caches;
using Todos.Applications.DTOs;
using Todos.Domain;

namespace Todos.Applications.Handlers.Commands.CreateTodo;

internal class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, GetTodoDto>
{
    private readonly IBaseWriteRepository<Todo> _todos;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly IMapper _mapper;
    
    private readonly ICleanTodosCacheService _cleanTodosCacheService;

    public CreateTodoCommandHandler(IBaseWriteRepository<Todo> todos, ICurrentUserService currentUserService, IMapper mapper, ICleanTodosCacheService cleanTodosCacheService)
    {
        _todos = todos;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _cleanTodosCacheService = cleanTodosCacheService;
    }

    public async Task<GetTodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Name = request.Name,
            OwnerId = _currentUserService.CurrentUserId!.Value,
            CreatedDate = DateTime.UtcNow
        };

        todo = await _todos.AddAsync(todo, cancellationToken);
        
        _cleanTodosCacheService.ClearListCaches();
        
        return _mapper.Map<GetTodoDto>(todo);
    }
}
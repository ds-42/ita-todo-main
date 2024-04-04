using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Todos.Applications.DTOs;
using Todos.Domain;

namespace Todos.Applications.Handlers.Commands.UpdateTodo;

[RequestAuthorize]
public class UpdateTodoCommand : IMapTo<Todo>, IRequest<GetTodoDto>
{
    public int TodoId { get; init; }

    public string Name { get; init; } = default!;

    public bool IsDone { get; init; }
}
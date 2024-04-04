using Core.Auth.Application.Attributes;
using MediatR;
using Todos.Applications.DTOs;

namespace Todos.Applications.Handlers.Commands.CreateTodo;

[RequestAuthorize]
public class CreateTodoCommand : IRequest<GetTodoDto>
{
    public string Name { get; init; } = default!;
}
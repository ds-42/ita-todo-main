using Core.Auth.Application.Attributes;
using MediatR;

namespace Todos.Applications.Handlers.Commands.DeleteTodo;

[RequestAuthorize]
public class DeleteTodoCommand : IRequest
{
    public int TodoId { get; init; }
}
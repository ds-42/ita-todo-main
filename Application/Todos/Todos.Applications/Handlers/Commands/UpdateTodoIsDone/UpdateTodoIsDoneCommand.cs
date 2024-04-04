using MediatR;
using Todos.Applications.DTOs;

namespace Todos.Applications.Handlers.Commands.UpdateTodoIsDone;

public class UpdateTodoIsDoneCommand : UpdateTodoIsDonePayload, IRequest<GetTodoDto>
{
    public int TodoId { get; init; }
}
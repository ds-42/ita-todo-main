using Core.Application.Abstractions.Mappings;

namespace Todos.Applications.Handlers.Commands.UpdateTodoIsDone;

public class UpdateTodoIsDonePayload
{
    public bool IsDone { get; init; }
}
using Core.Application.Abstractions.Mappings;

namespace Todos.Applications.Handlers.Commands.UpdateTodo;

public class UpdateTodoPayload
{
    public string Name { get; set; } = default!;

    public bool IsDone { get; set; }
}
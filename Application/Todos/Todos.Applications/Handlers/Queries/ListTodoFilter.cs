namespace Todos.Applications.Handlers.Queries;

public class ListTodoFilter
{
    public string? FreeText { get; init; }
    
    public bool? IsDone { get; init; }
}
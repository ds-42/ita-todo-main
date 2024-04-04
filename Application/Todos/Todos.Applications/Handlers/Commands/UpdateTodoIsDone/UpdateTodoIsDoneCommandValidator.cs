using FluentValidation;

namespace Todos.Applications.Handlers.Commands.UpdateTodoIsDone;

internal class UpdateTodoIsDoneCommandValidator : AbstractValidator<UpdateTodoIsDoneCommand>
{
    public UpdateTodoIsDoneCommandValidator()
    {
        RuleFor(e => e.TodoId).GreaterThan(0);
    }
}
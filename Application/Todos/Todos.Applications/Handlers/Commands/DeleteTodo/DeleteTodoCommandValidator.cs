using FluentValidation;

namespace Todos.Applications.Handlers.Commands.DeleteTodo;

internal class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(e => e.TodoId).GreaterThan(0);
    }
}
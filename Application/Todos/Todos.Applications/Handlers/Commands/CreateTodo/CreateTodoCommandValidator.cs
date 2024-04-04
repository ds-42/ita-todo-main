using FluentValidation;

namespace Todos.Applications.Handlers.Commands.CreateTodo;

internal class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
    }
}
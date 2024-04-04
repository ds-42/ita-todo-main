using FluentValidation;

namespace Todos.Applications.Handlers.Queries.GetTodosCount;

internal class GetTodosCountQueryValidator : AbstractValidator<GetTodosCountQuery>
{
    public GetTodosCountQueryValidator()
    {
        RuleFor(e => e).IsValidListTodoFilter();
    }
}
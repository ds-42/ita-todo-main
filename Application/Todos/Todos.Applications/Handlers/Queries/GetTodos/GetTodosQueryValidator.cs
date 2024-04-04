using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Todos.Applications.Handlers.Queries.GetTodos;

internal class GetTodosQueryValidator : AbstractValidator<GetTodosQuery>
{
    public GetTodosQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListTodoFilter();
    }
}
using FluentValidation;

namespace Todos.Applications.Handlers.Queries;

internal class BaseListTodoFilterValidator : AbstractValidator<ListTodoFilter>
{
    public BaseListTodoFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListTodoFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListTodoFilter> IsValidListTodoFilter<T>(this IRuleBuilder<T, ListTodoFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new BaseListTodoFilterValidator());
    }
}
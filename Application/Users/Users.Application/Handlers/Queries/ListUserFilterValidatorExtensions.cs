using FluentValidation;

namespace Users.Application.Handlers.Queries;

internal class BaseListUserFilterValidator : AbstractValidator<ListUserFilter>
{
    public BaseListUserFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListUserFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListUserFilter> IsValidListUserFilter<T>(this IRuleBuilder<T, ListUserFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new BaseListUserFilterValidator());
    }
}
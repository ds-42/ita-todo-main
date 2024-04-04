using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.ValidatorsExtensions;

internal sealed class BasePaginationFilterValidator : AbstractValidator<IBasePaginationFilter>
{
    public BasePaginationFilterValidator()
    {
        RuleFor(r => r.Limit).GreaterThan(0).LessThanOrEqualTo(20).When(r => r.Limit.HasValue);
        RuleFor(r => r.Offset).GreaterThan(0).When(r => r.Offset.HasValue);
    }
}

public static class PaginationFilterValidatorExtensions
{
    public static void IsValidPaginationFilter<T>(this IRuleBuilder<T, IBasePaginationFilter> ruleBuilder)
    {
        ruleBuilder
            .NotNull()
            .SetValidator(new BasePaginationFilterValidator());
    }
}
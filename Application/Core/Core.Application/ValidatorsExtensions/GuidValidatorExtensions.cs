using FluentValidation;

namespace Core.Application.ValidatorsExtensions;

public static class GuidValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(e => Guid.TryParse(e, out _)).WithErrorCode("Not a guid");
    }
}
using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Xunit;
using Xunit.Abstractions;

namespace Core.Tests;


public abstract class ValidatorTestBase<TValidatable> : TestBase
{
    protected ValidatorTestBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected abstract IValidator<TValidatable> TestValidator { get; }

    protected void AssertValid(TValidatable request)
    {
        var validationResult = TestValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                WriteOutput(error.ToString());
            }

            WriteOutput(JsonSerializer.Serialize(request));
        }

        Assert.True(validationResult.IsValid);
    }

    protected ValidationResult AssertNotValid(TValidatable request)
    {
        var validationResult = TestValidator.Validate(request);

        if (validationResult.IsValid)
        {
            WriteOutput(JsonSerializer.Serialize(request));
        }

        Assert.False(validationResult.IsValid);

        return validationResult;
    }
}
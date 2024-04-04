using Core.ArchitectureTests.Utils;
using FluentValidation;
using NetArchTest.Rules;

namespace Core.ArchitectureTests.Tests;

public class ValidatorTests
{
    [Fact]
    public void Validator_Should_Has_ValidatorSuffix()
    {
        foreach (var solutionAssembly in SolutionUtils.GetSolutionAssemblies())
        {
            TestUtils.AssertResult(Types.InAssembly(solutionAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .HaveNameEndingWith("Validator")
                .GetResult());
        }
    }

    [Fact]
    public void Validators_Should_Not_Be_Public()
    {
        foreach (var solutionAssembly in SolutionUtils.GetSolutionAssemblies())
        {
            TestUtils.AssertResult(Types.InAssembly(solutionAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .NotBePublic()
                .GetResult());   
        }
    }
}
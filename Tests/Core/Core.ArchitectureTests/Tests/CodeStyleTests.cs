using NetArchTest.Rules;
using Core.ArchitectureTests.CustomRules;
using Core.ArchitectureTests.Utils;

namespace Core.ArchitectureTests.Tests;

public class CodeStyleTests
{
    [Fact]
    public void Interfaces_Should_StartWith_I_Letter()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult());
    }

    [Fact]
    public void Private_Fields_Should_StartWith_Underscore()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreClasses()
            .Should()
            .MeetCustomRule(new PrivateFieldNamingRule())
            .GetResult());
    }
}
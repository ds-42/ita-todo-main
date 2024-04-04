using FluentAssertions;
using NetArchTest.Rules;

namespace Core.ArchitectureTests.Utils;

public static class TestUtils
{
    public static void AssertResult(TestResult result)
    {
        var badTypes = result.FailingTypes ?? new List<Type>();
        foreach (var failingType in badTypes) Console.WriteLine($"Failing Type '{failingType}'");
        result.FailingTypes.Should().BeNullOrEmpty();
    }
    
    public static void AssertImmutable(IEnumerable<Type> types)
    {
        IList<Type> badTypes = new List<Type>();

        foreach (var type in types)
            if (type.GetFields().Any(x => !x.IsPrivate) ||
                type.GetProperties().Any(x => !x.IsInitOnly()))
            {
                badTypes.Add(type);
                break;
            }

        foreach (var badType in badTypes) Console.WriteLine($"Failing Type '{badType}'");
        badTypes.Should().BeEmpty();
    }
}
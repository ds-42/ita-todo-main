using Core.Application.BaseRealizations;
using Core.ArchitectureTests.Utils;
using MediatR;
using NetArchTest.Rules;

namespace Core.ArchitectureTests.Tests;

public class RequestTests
{
    [Fact]
    public void Request_Should_Has_Suffix()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .ImplementInterface(typeof(IRequest))
            .Or().ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Command")
            .Or()
            .HaveNameEndingWith("Query")
            .GetResult());
    }

    [Fact]
    public void Request_Should_Be_Immutable()
    {
        TestUtils.AssertImmutable(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .And()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .ImplementInterface(typeof(IRequest<>))
            .GetTypes());
    }
    
    [Fact]
    public void Request_NotNullableFields_Should_Be_Required()
    {
        TestUtils.AssertImmutable(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .And()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .ImplementInterface(typeof(IRequest<>))
            .GetTypes());
    }

    [Fact]
    public void Request_Should_Be_Public()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .And()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .BePublic()
            .GetResult());
    }

    [Fact]
    public void Request_Should_Be_PlacedInCommandsOrQueriesFolder()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .ImplementInterface(typeof(IRequest))
            .Or().ImplementInterface(typeof(IRequest<>))
            .Should()
            .ResideInNamespaceContaining("Handlers")
            .And()
            .ResideInNamespaceContaining("Commands")
            .Or()
            .ResideInNamespaceContaining("Queries")
            .GetResult());
    }

    [Fact]
    public void RequestHandler_Should_Has_CommandOrQueryHandlerSuffix()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreNotAbstract()
            .And()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .Or()
            .HaveNameEndingWith("QueryHandler")
            .GetResult());
    }

    [Fact]
    public void RequestHandler_Should_Not_Be_Public()
    {
        TestUtils.AssertResult(Types.InAssemblies(SolutionUtils.GetSolutionAssemblies())
            .That()
            .AreNotAbstract()
            .And()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .Inherit(typeof(BaseCashedForUserQuery<,>))
            .Should()
            .NotBePublic()
            .GetResult());
    }
}
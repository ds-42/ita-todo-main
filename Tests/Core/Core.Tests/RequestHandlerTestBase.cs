using Core.Application.Exceptions;
using Core.Auth.Application.Exceptions;
using FluentAssertions;
using FluentAssertions.Specialized;
using MediatR;
using Xunit.Abstractions;

namespace Core.Tests;

public abstract class RequestHandlerTestBase<TCommand> : TestBase where TCommand : IRequest
{
    protected RequestHandlerTestBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
    
    protected abstract IRequestHandler<TCommand> CommandHandler { get; }
    
    protected async Task AssertNotThrow(TCommand command)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should().NotThrowAsync();
    }

    protected async Task<ExceptionAssertions<TException>> AssertThrow<TException>(TCommand command)
        where TException : Exception
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        return await action.Should().ThrowAsync<TException>();
    }

    protected async Task AssertThrowBadOperation(TCommand command, string expectedMessage)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should()
            .ThrowAsync<BadOperationException>()
            .WithMessage(expectedMessage);
    }

    protected async Task AssertThrowNotFound(TCommand command)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    protected async Task AssertThrowForbiddenFound(TCommand command)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should().ThrowAsync<ForbiddenException>();
    }
}
public abstract class RequestHandlerTestBase<TCommand, TResult> : TestBase
    where TCommand : IRequest<TResult>
{
    protected RequestHandlerTestBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected abstract IRequestHandler<TCommand, TResult> CommandHandler { get; }
    
    protected async Task AssertNotThrow(TCommand command)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should().NotThrowAsync();
    }

    protected async Task<ExceptionAssertions<TException>> AssertThrow<TException>(TCommand command)
        where TException : Exception
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        return await action.Should().ThrowAsync<TException>();
    }

    protected async Task AssertThrowBadOperation(TCommand command, string expectedMessage)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should()
            .ThrowAsync<BadOperationException>()
            .WithMessage(expectedMessage);
    }

    protected async Task AssertThrowNotFound(TCommand command)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    protected async Task AssertThrowForbiddenFound(TCommand command)
    {
        // act
        var action = () => CommandHandler.Handle(command, default);

        // assert
        await action.Should().ThrowAsync<ForbiddenException>();
    }
}
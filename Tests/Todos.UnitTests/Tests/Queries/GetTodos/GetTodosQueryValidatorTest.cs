using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Todos.Applications.Handlers.Queries.GetTodos;
using Xunit.Abstractions;

namespace Todos.UnitTests.Tests.Queries.GetTodos;

public class GetTodosQueryValidatorTest: ValidatorTestBase<GetTodosQuery>
{
    public GetTodosQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetTodosQuery> TestValidator =>
        TestFixture.Create<GetTodosQueryValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var query = new GetTodosQuery
        {
            Limit = 5,
            Offset = 10,
            FreeText = "123",
            IsDone = true
        };

        // act & assert
        AssertValid(query);
    }
    
    [Theory]
    [FixtureInlineAutoData(1)]
    [FixtureInlineAutoData(10)]
    [FixtureInlineAutoData(20)]
    public void Should_NotValid_When_LimitIsValid(int limit)
    {
        // arrange
        var query = new GetTodosQuery
        {
            Limit = limit,
        };

        // act & assert
        AssertValid(query);
    }
    
    [Theory]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(-1)]
    [FixtureInlineAutoData(21)]
    public void Should_NotBeValid_When_IncorrectLimit(int limit)
    {
        // arrange
        var query = new GetTodosQuery
        {
            Limit = limit,
        };

        // act & assert
        AssertNotValid(query);
    }
    
    [Theory]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(-1)]
    public void Should_NotBeValid_When_IncorrectOffset(int offset)
    {
        // arrange
        var query = new GetTodosQuery
        {
            Offset = offset,
        };

        // act & assert
        AssertNotValid(query);
    }
    
    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("123")]
    [FixtureInlineAutoData("1#*&^%$#@#$%±~`}{][\\|?/.,<>")]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_FreeTextIsValid(string freeText)
    {
        // arrange
        var query = new GetTodosQuery
        {
            FreeText = freeText,
        };

        // act & assert
        AssertValid(query);
    }
    
    [Fact]
    public void Should_NotBeValid_When_FreeTextIsGreatThen50()
    {
        // arrange
        var query = new GetTodosQuery
        {
            FreeText = "123456789012345678901234567890123456789012345678901",
        };

        // act & assert
        AssertNotValid(query);
    }
}
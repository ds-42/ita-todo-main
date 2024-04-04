using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Users.Application.Handlers.Queries.GetUser;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Queries.GetUser;

public class GetUserQueryValidatorTests : ValidatorTestBase<GetUserQuery>
{
    public GetUserQueryValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetUserQuery> TestValidator =>
        TestFixture.Create<GetUserQueryValidator>();
    
    [Theory, FixtureAutoData]
    public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(Guid id)
    {
        // arrange
        var query = new GetUserQuery
        {
            Id = id.ToString()
        };

        // act & assert
        AssertValid(query);
    }
    
    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("123")]
    public void Should_NotBeValid_When_IncorrectGuid(string id)
    {
        // arrange
        var query = new GetUserQuery
        {
            Id = id
        };

        // act & assert
        AssertNotValid(query);
    }
}
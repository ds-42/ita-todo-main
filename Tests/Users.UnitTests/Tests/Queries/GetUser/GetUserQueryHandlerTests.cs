using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Tests;
using Core.Tests.Fixtures;
using Core.Users.Domain;
using MediatR;
using Moq;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Handlers.Queries.GetUser;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Queries.GetUser;

public class GetUserQueryHandlerTests : RequestHandlerTestBase<GetUserQuery, GetUserDto>
{
    private readonly Mock<IBaseReadRepository<ApplicationUser>> _usersMok = new();
    
    private readonly Mock<ApplicationUserMemoryCache> _mockUserMemoryCache = new();
    
    private readonly IMapper _mapper;

    public GetUserQueryHandlerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetUserQuery).Assembly).Mapper;
    }

    protected override IRequestHandler<GetUserQuery, GetUserDto> CommandHandler =>
        new GetUserQueryHandler(_usersMok.Object, _mapper, _mockUserMemoryCache.Object);
    
    [Fact]
    public async Task Should_BeValid_When_UserFounded()
    {
        // arrange
        var userId = Guid.NewGuid();
        var query = new GetUserQuery()
        {
            Id = userId.ToString()
        };
        
        var user = TestFixture.Build<ApplicationUser>().Create();
        user.ApplicationUserId = userId;
        
        _usersMok
            .Setup(p =>
                p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)
            ).ReturnsAsync(user);

        // act and assert
        await AssertNotThrow(query);
    }
    
    [Fact]
    public async Task Should_ThrowNotFound_When_UserNotFound()
    {
        // arrange
        var query = new GetUserQuery()
        {
            Id = Guid.NewGuid().ToString()
        };
        
        _usersMok
            .Setup(p =>
                p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)
            ).ReturnsAsync(null as ApplicationUser);

        // act and assert
        await AssertThrowNotFound(query);
    }
}
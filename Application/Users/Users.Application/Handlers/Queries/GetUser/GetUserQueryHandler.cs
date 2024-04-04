using AutoMapper;
using Core.Application.Abstractions.Persistence;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Users.Domain;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUser;

internal class GetUserQueryHandler : BaseCashedQuery<GetUserQuery, GetUserDto>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;
    

    public GetUserQueryHandler(IBaseReadRepository<ApplicationUser> users, IMapper mapper, ApplicationUserMemoryCache cache) : base(cache)
    {
        _users = users;
        _mapper = mapper;
    }

    public override async Task<GetUserDto> SentQueryAsync(GetUserQuery request, CancellationToken cancellationToken)
    {
        var idGuid = Guid.Parse(request.Id);
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == idGuid, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(request);
        }

        return _mapper.Map<GetUserDto>(user);
    }
}
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Users.Domain;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUsers;

internal class GetUsersQueryHandler : BaseCashedQuery<GetUsersQuery, BaseListDto<GetUserDto>>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;
    
    private readonly IMapper _mapper;
    
    public GetUsersQueryHandler(IBaseReadRepository<ApplicationUser> users, IMapper mapper, ApplicationUsersListMemoryCache cache) : base(cache)
    {
        _users = users;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetUserDto>> SentQueryAsync(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _users.AsQueryable().Where(ListWhere.Where(request));
        
        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }
        
        query = query.OrderBy(e => e.ApplicationUserId);

        var entitiesResult = await _users.AsAsyncRead().ToArrayAsync(query, cancellationToken);
        var entitiesCount = await _users.AsAsyncRead().CountAsync(query, cancellationToken);

        var items = _mapper.Map<GetUserDto[]>(entitiesResult);
        return new BaseListDto<GetUserDto>
        {
            Items = items,
            TotalCount = entitiesCount
        };
    }
}
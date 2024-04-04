using Auth.Application.Dtos;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain;
using MediatR;

namespace Auth.Application.Handlers.Queries.GetCurrentUser;

internal class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, GetUserDto>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        IBaseReadRepository<ApplicationUser> users, 
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _users = users;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<GetUserDto> Handle(GetCurrentUserQuery request,  CancellationToken cancellationToken)
    {
        var user = await _users.AsAsyncRead()
            .SingleOrDefaultAsync(e => e.ApplicationUserId == _currentUserService.CurrentUserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException($"User with id {_currentUserService.CurrentUserId}");
        }

        return _mapper.Map<GetUserDto>(user);
    }
}
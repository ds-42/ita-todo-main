using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using Core.Auth.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Commands.UpdateUser;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserDto>
{
    private readonly IBaseWriteRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;
    
    private readonly ApplicationUsersListMemoryCache _applicationUsersListMemoryCache;
    
    private readonly ApplicationUsersCountMemoryCache _applicationUsersCountMemoryCache;
    
    private readonly ApplicationUserMemoryCache _applicationUserMemoryCache;
    
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IBaseWriteRepository<ApplicationUser> users, IMapper mapper,
        ICurrentUserService currentUserService,
        ApplicationUsersListMemoryCache applicationUsersListMemoryCache,
        ApplicationUsersCountMemoryCache applicationUsersCountMemoryCache,
        ApplicationUserMemoryCache applicationUserMemoryCache,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _users = users;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _applicationUsersListMemoryCache = applicationUsersListMemoryCache;
        _applicationUsersCountMemoryCache = applicationUsersCountMemoryCache;
        _applicationUserMemoryCache = applicationUserMemoryCache;
        _logger = logger;
    }

    public async Task<GetUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.Id);

        if (_currentUserService.CurrentUserId != userId &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(request);
        }

        _mapper.Map(request, user);
        user.UpdatedDate = DateTime.UtcNow;
        user = await _users.UpdateAsync(user, cancellationToken);
        var result = _mapper.Map<GetUserDto>(user);
        
        _applicationUsersListMemoryCache.Clear();
        _applicationUsersCountMemoryCache.Clear();
        _applicationUserMemoryCache.Set(new GetUserDto {ApplicationUserId = user.ApplicationUserId}, result, 1);
        _logger.LogWarning($"User {user.ApplicationUserId.ToString()} updated by {_currentUserService.CurrentUserId.ToString()}");

        return result;
    }
}
using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Caches;
using Users.Application.Handlers.Queries.GetUser;

namespace Users.Application.Handlers.Commands.DeleteUser;

internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IBaseWriteRepository<ApplicationUser> _users;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly ApplicationUsersListMemoryCache _listCache;
    
    private readonly ApplicationUsersCountMemoryCache _countCache;
    
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    private readonly ApplicationUserMemoryCache _userCase;

    public DeleteUserCommandHandler(
        IBaseWriteRepository<ApplicationUser> users, 
        ICurrentUserService currentUserService, 
        ApplicationUsersListMemoryCache listCache,
        ApplicationUsersCountMemoryCache countCache,
        ILogger<DeleteUserCommandHandler> logger,
        ApplicationUserMemoryCache userCase)
    {
        _users = users;
        _currentUserService = currentUserService;
        _listCache = listCache;
        _countCache = countCache;
        _logger = logger;
        _userCase = userCase;
    }
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.Id);
        
        if (userId != _currentUserService.CurrentUserId &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(request);
        }

        await _users.RemoveAsync(user, cancellationToken);
        _listCache.Clear();
        _countCache.Clear();
        _logger.LogWarning($"User {user.ApplicationUserId.ToString()} deleted by {_currentUserService.CurrentUserId.ToString()}");
        _userCase.DeleteItem(new GetUserQuery {Id = user.ApplicationUserId.ToString()});
    }
}
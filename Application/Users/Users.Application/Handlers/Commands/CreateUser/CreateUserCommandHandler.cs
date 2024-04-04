using AutoMapper;
using Core.Application.Abstractions.Persistence;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Utils;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Commands.CreateUser;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserDto>
{
    private readonly IBaseWriteRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;
    
    private readonly ApplicationUsersListMemoryCache _listCache;
    
    private readonly ILogger<CreateUserCommandHandler> _logger;

    private readonly ApplicationUsersCountMemoryCache _countCache;

    public CreateUserCommandHandler(IBaseWriteRepository<ApplicationUser> users, IMapper mapper,
        ApplicationUsersListMemoryCache listCache,
        ILogger<CreateUserCommandHandler> logger,
        ApplicationUsersCountMemoryCache countCache)
    {
        _users = users;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
    }

    public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await _users.AsAsyncRead().AnyAsync(e => e.Login == request.Login, cancellationToken);
        if (isUserExist)
        {
            throw new BadOperationException($"User with login {request.Login} already exists.");
        }

        var user = new ApplicationUser
        {
            Login = request.Login,
            PasswordHash = PasswordHashUtil.Hash(request.Password),
            CreatedDate = DateTime.UtcNow,
            Roles = new[]
            {
                new ApplicationUserApplicationUserRole
                {
                    ApplicationUserRoleId = (int)ApplicationUserRolesEnum.Client
                }
            }
        };

        user = await _users.AddAsync(user, cancellationToken);
        
        _listCache.Clear();
        _countCache.Clear();
        
        _logger.LogInformation($"New user {user.ApplicationUserId} created.");
        
        return _mapper.Map<GetUserDto>(user);
    }
}
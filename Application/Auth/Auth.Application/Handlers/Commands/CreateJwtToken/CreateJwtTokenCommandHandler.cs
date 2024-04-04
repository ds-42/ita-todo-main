using Auth.Application.Dtos;
using Auth.Application.Services;
using Auth.Domain;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Exceptions;
using Core.Auth.Application.Utils;
using Core.Users.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Auth.Application.Handlers.Commands.CreateJwtToken;

internal class CreateJwtTokenCommandHandler : IRequestHandler<CreateJwtTokenCommand, JwtTokenDto>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;
    private readonly IBaseWriteRepository<RefreshToken> _refreshTokens;
    private readonly ICreateJwtTokenService _createJwtTokenService;
    private readonly IConfiguration _configuration;

    public CreateJwtTokenCommandHandler(
        IBaseReadRepository<ApplicationUser> users, 
        IBaseWriteRepository<RefreshToken> refreshTokens,
        ICreateJwtTokenService createJwtTokenService,
        IConfiguration configuration)
    {
        _users = users;
        _refreshTokens = refreshTokens;
        _createJwtTokenService = createJwtTokenService;
        _configuration = configuration;
    }
    
    public async Task<JwtTokenDto> Handle(CreateJwtTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(u => u.Login == request.Login.Trim(), cancellationToken);
        if (user is null)
        {
            throw new NotFoundException($"User with login {request.Login} don't exist");
        }
        
        if (!PasswordHashUtil.Verify(request.Password, user.PasswordHash))
        {
            throw new ForbiddenException();
        }

        var jwtTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:JwtToken"]!));
        var refreshTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:RefreshToken"]!));
        var token = _createJwtTokenService.CreateJwtToken(user, jwtTokenDateExpires);
        var refreshToken = await _refreshTokens.AddAsync(new RefreshToken { ApplicationUserId = user.ApplicationUserId, Expired = refreshTokenDateExpires}, cancellationToken);
        
        return new JwtTokenDto
        {
            JwtToken = token,
            RefreshToken = refreshToken.ApplicationUserId.ToString(),
            Expires = jwtTokenDateExpires
        };
    }
}
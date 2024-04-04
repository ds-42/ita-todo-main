using Auth.Application.Dtos;
using Auth.Application.Handlers.Commands.CreateJwtToken;
using Auth.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;
using Core.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Apis;

/// <summary>
/// Auth api.
/// </summary>
public class AuthApi : IApi
{
    const string Tag = "Auth";
    
    /// <summary>
    /// Register auth apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        #region Command
        
        app.MapPost($"{baseApiUrl}/LoginJwt", LoginJwt)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Create JWT token")
            .Produces<JwtTokenDto>();
        
        app.MapPost($"{baseApiUrl}/RefreshJwt", RefreshJwt)
            .RequireAuthorization()
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update JWT token by refresh token")
            .Produces<JwtTokenDto>();
        
        #endregion
    }
    

    private static Task<JwtTokenDto> LoginJwt([FromBody] CreateJwtTokenCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        return mediator.Send(command, cancellationToken);
    }
    
    private static Task<JwtTokenDto> RefreshJwt([FromBody] CreateJwtTokenByRefreshTokenCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        return mediator.Send(command, cancellationToken);
    }
}
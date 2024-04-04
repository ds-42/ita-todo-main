using Auth.Application.Dtos;
using Auth.Application.Handlers.Queries.GetCurrentUser;
using Core.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Apis;

/// <summary>
/// Users Api.
/// </summary>
public class UsersApi : IApi
{
    const string Tag = "Users";

    /// <summary>
    /// Register users apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        #region Queries

        app.MapGet($"{baseApiUrl}/{Tag}/Current", GetCurrentUser)
            .WithTags(Tag)
            .WithSummary("Get current user")
            .WithOpenApi()
            .RequireAuthorization()
            .Produces<GetUserDto>();

        #endregion
    }
    
    private static Task<GetUserDto> GetCurrentUser(IMediator mediator, CancellationToken cancellationToken)
    {
        return mediator.Send(new GetCurrentUserQuery(), cancellationToken);
    }
}
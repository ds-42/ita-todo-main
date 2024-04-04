using System.Net;
using Core.Application.Abstractions;
using Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Applications.DTOs;
using Todos.Applications.Handlers.Commands.CreateTodo;
using Todos.Applications.Handlers.Commands.DeleteTodo;
using Todos.Applications.Handlers.Commands.UpdateTodo;
using Todos.Applications.Handlers.Commands.UpdateTodoIsDone;
using Todos.Applications.Handlers.Queries.GetTodo;
using Todos.Applications.Handlers.Queries.GetTodos;
using Todos.Applications.Handlers.Queries.GetTodosCount;

namespace Todos.Api.Apis;

/// <summary>
/// Todos Api.
/// </summary>
public class TodosApi: IApi
{
    const string Tag = "Todos";
    
    private string _apiUrl = default!;

    /// <summary>
    /// Register todos apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        _apiUrl = $"{baseApiUrl}/{Tag}";
        
        #region Queries

        app.MapGet($"{_apiUrl}/{{id}}", GetTodo)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get todo")
            .Produces<BaseListDto<GetTodoDto>>()
            .RequireAuthorization();

        app.MapGet(_apiUrl, GetTodos)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get todos")
            .Produces<GetTodoDto[]>()
            .RequireAuthorization();
        
        app.MapGet($"{_apiUrl}/Count", GetTodosCount)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get todos count")
            .Produces<int>()
            .RequireAuthorization();

        #endregion

        #region Command
        
        app.MapPost(_apiUrl, PostTodo)
            .WithTags(Tag)
            .Produces<GetTodoDto>((int)HttpStatusCode.Created)
            .WithOpenApi()
            .WithSummary("Create todo")
            .RequireAuthorization();
        
        app.MapPut($"{_apiUrl}/{{id}}", PutTodo)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update todo")
            .RequireAuthorization()
            .Produces<GetTodoDto>();
        
        app.MapPatch($"{_apiUrl}/{{id}}/IsDone", PatchIsDone)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update todo IsDone")
            .RequireAuthorization()
            .Produces<GetTodoDto>();
        
        app.MapDelete($"{_apiUrl}/{{id}}", DeleteTodo)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Delete user")
            .RequireAuthorization();

        #endregion
    }
    
    private Task<int> GetTodosCount([FromServices] IMediator mediator, [AsParameters] GetTodosCountQuery query, CancellationToken cancellationToken)
    {
        return mediator.Send(query, cancellationToken);
    }

    private static Task<GetTodoDto> GetTodo([FromServices] IMediator mediator, [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetTodoQuery()
        {
            TodoId = id
        }, cancellationToken);
    }
    
    private static async Task<GetTodoDto[]> GetTodos(HttpContext httpContext, [FromServices] IMediator mediator, [AsParameters] GetTodosQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        httpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        return result.Items;
    }
    
    private async Task<IResult> PostTodo([FromServices] IMediator mediator, [FromBody] CreateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created( $"{_apiUrl}/{{}}", result);
    }
    
    private static Task<GetTodoDto> PutTodo([FromServices] IMediator mediator, [FromRoute] int id, [FromBody] UpdateTodoPayload payload,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTodoCommand()
        {
            TodoId = id,
            Name = payload.Name,
            IsDone = payload.IsDone
        };
        return mediator.Send(command, cancellationToken);
    }
    
    private Task<GetTodoDto> PatchIsDone([FromServices] IMediator mediator, [FromRoute] int id, [FromBody] UpdateTodoIsDonePayload payload, CancellationToken cancellationToken)
    {
        var command = new UpdateTodoIsDoneCommand()
        {
            TodoId = id,
            IsDone = payload.IsDone
        };
        return mediator.Send(command, cancellationToken);
    }
    
    private static Task DeleteTodo([FromServices] IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
    {
        return mediator.Send(new DeleteTodoCommand { TodoId = id }, cancellationToken);
    }
}
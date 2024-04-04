using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;

namespace Todos.Applications.Handlers.Queries.GetTodos;

[RequestAuthorize]
public class GetTodosQuery : ListTodoFilter, IBasePaginationFilter, IRequest<BaseListDto<DTOs.GetTodoDto>>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
}
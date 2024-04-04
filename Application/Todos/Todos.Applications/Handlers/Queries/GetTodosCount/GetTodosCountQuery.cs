using Core.Auth.Application.Attributes;
using MediatR;

namespace Todos.Applications.Handlers.Queries.GetTodosCount;

[RequestAuthorize]
public class GetTodosCountQuery : ListTodoFilter, IRequest<int>
{
    
}
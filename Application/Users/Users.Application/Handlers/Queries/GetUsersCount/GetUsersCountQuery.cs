using MediatR;

namespace Users.Application.Handlers.Queries.GetUsersCount;

public class GetUsersCountQuery : ListUserFilter, IRequest<int>
{
    
}
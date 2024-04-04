using Core.Application.DTOs;
using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUsers;

public class GetUsersQuery : ListUserFilter, IBasePaginationFilter, IRequest<BaseListDto<GetUserDto>>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
}
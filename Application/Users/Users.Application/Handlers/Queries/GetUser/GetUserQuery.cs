using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUser;

public class GetUserQuery : IRequest<GetUserDto>
{
    public string Id { get; init; } = default!;
}
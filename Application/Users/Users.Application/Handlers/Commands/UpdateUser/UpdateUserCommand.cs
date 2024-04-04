using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Users.Domain;
using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Commands.UpdateUser;

[RequestAuthorize]
public class UpdateUserCommand : IRequest<GetUserDto>, IMapTo<ApplicationUser>
{
    public string Id { get; init; } = default!;

    public string Login { get; init; } = default!;
}
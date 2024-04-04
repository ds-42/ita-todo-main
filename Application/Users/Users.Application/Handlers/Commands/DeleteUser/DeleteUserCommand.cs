using Core.Auth.Application.Attributes;
using MediatR;

namespace Users.Application.Handlers.Commands.DeleteUser;

[RequestAuthorize]
public class DeleteUserCommand : IRequest
{
    public string Id { get; init; } = default!;
}
using MediatR;

namespace Users.Application.Handlers.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommand : IRequest
{
    public string UserId { get; init; } = default!;

    public string Password { get; init; } = default!;
}
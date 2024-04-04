namespace Users.Application.Handlers.Commands.UpdateUserPassword;

public class UpdateUserPasswordPayload
{
    public required string Password { get; init; } = default!;
}
namespace Users.Application.Handlers.Commands.UpdateUser;

public class UpdateUserPayload
{
    public required string Login { get; init; } = default!;
}
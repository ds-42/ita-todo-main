using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Commands.CreateUser;

public class CreateUserCommand : IRequest<GetUserDto>
{
    public string Login { get; init; } = default!;

    public string Password { get; init; } = default!;
}
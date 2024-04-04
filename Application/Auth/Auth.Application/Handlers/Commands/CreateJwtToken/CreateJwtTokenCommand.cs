using Auth.Application.Dtos;
using MediatR;

namespace Auth.Application.Handlers.Commands.CreateJwtToken;

public class CreateJwtTokenCommand : IRequest<JwtTokenDto>
{
    public string Login { get; init; } = default!;

    public string Password { get; init; } = default!;
}
using Auth.Application.Dtos;
using MediatR;

namespace Auth.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommand : IRequest<JwtTokenDto>
{
    public string RefreshToken { get; init; } = default!;
}
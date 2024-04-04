using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Auth.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

internal class CreateJwtTokenByRefreshTokenCommandValidator : AbstractValidator<CreateJwtTokenByRefreshTokenCommand>
{
    public CreateJwtTokenByRefreshTokenCommandValidator()
    {
        RuleFor(e => e.RefreshToken).NotEmpty().IsGuid();
    }
}
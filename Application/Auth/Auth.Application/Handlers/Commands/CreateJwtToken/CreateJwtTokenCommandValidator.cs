using FluentValidation;

namespace Auth.Application.Handlers.Commands.CreateJwtToken;

internal class CreateJwtTokenCommandValidator : AbstractValidator<CreateJwtTokenCommand>
{
    public CreateJwtTokenCommandValidator()
    {
        RuleFor(e => e.Login).MinimumLength(3).MaximumLength(50).NotEmpty();
        RuleFor(e => e.Password).MinimumLength(8).MaximumLength(100).NotEmpty();
    }
}
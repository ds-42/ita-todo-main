using FluentValidation;

namespace Users.Application.Handlers.Commands.UpdateUserPassword;

internal class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(e => e.Password).MinimumLength(8).MaximumLength(100).NotEmpty();
    }
}
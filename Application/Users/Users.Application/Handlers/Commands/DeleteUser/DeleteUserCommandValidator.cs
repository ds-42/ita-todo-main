using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Users.Application.Handlers.Commands.DeleteUser;

internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty().IsGuid();
    }
}
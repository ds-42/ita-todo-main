using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Users.Application.Handlers.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(e => e.Id).NotEmpty().IsGuid();
    }
}
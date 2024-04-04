using FluentValidation;

namespace Users.Application.Handlers.Queries.GetUsersCount;

internal class GetUsersCountQueryValidator : AbstractValidator<GetUsersCountQuery>
{
    public GetUsersCountQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
    }
}
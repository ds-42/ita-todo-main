using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Users.Application.Handlers.Queries.GetUsers;

internal class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
        RuleFor(e => e).IsValidPaginationFilter();
    }
    
}
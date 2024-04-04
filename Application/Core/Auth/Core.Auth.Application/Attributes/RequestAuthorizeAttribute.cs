using Core.Users.Domain.Enums;

namespace Core.Auth.Application.Attributes;

public class RequestAuthorizeAttribute(ApplicationUserRolesEnum[]? roles = null) : Attribute
{
    public ApplicationUserRolesEnum[]? Roles { get; } = roles;
}
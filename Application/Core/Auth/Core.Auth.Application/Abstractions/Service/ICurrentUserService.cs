using Core.Users.Domain;
using Core.Users.Domain.Enums;

namespace Core.Auth.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public ApplicationUserRolesEnum[] CurrentUserRoles { get; }

    public bool UserInRole(ApplicationUserRolesEnum role);
}
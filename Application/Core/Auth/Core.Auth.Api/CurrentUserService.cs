using System.Security.Claims;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;

namespace Core.Auth.Api;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) 
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? CurrentUserId
    {
        get
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return null;
            }

            return Guid.Parse(userId);
        }
    }

    public bool UserInRole(ApplicationUserRolesEnum role)
    {
        return CurrentUserRoles.Contains(role);
    }

    public ApplicationUserRolesEnum[] CurrentUserRoles => _httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value)
        .Select(Enum.Parse<ApplicationUserRolesEnum>)
        .ToArray();
}
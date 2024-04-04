namespace Core.Users.Domain;

public class ApplicationUserApplicationUserRole
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;

    public int ApplicationUserRoleId { get; set; }
    public ApplicationUserRole Role { get; set; } = default!;
}
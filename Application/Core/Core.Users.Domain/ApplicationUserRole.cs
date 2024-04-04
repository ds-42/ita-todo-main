namespace Core.Users.Domain;

public class ApplicationUserRole
{
    public int ApplicationUserRoleId { get; set; }

    public string Name { get; set; } = default!;

    public IEnumerable<ApplicationUserApplicationUserRole> Users { get; set; } = default!;
}
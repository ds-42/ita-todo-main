namespace Core.Users.Domain;

public class ApplicationUser
{
    public Guid ApplicationUserId { get; set; } = default!;

    public string Login { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? LastSingInDate { get; set; }

    public IEnumerable<ApplicationUserApplicationUserRole> Roles { get; set; } =
        new List<ApplicationUserApplicationUserRole>();
}
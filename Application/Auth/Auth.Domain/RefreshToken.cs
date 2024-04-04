using Core.Users.Domain;

namespace Auth.Domain;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
    
    public DateTime Expired { get; set; }
}
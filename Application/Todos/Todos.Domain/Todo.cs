using Core.Users.Domain;

namespace Todos.Domain;

public class Todo
{
    public int TodoId { get; set; }

    public string Name { get; set; } = default!;
    
    public bool IsDone { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
    
    public Guid OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = default!;
}
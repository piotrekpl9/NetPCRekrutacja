namespace Domain.Entity;
using Primitives;

public class User : Entity
{
    public User(Guid id) : base(id)
    {
        
    }
    public User(Guid id, string email) : base(id)
    {
        Email = email;
    }

    public string Email { get; set; }
}
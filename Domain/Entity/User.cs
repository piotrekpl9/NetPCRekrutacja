namespace Domain.Entity;
using Primitives;

public class User : Entity
{
    public User()
    {
        
    }
    public User(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
}
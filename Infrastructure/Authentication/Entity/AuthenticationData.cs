namespace Infrastructure.Authentication.Entity;

public class AuthenticationData : Domain.Primitives.Entity
{
    public AuthenticationData(Guid id) : base(id)
    {
        
    }
    public AuthenticationData(Guid id, string password) : base(id)
    {
        Password = password;
    }

    public string Password { get; set; }
}
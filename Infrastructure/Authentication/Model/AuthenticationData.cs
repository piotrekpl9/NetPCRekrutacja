using Domain.Primitives;

namespace Infrastructure.Authentication.Model;

public class AuthenticationData : Entity
{
    public AuthenticationData(Guid userId, string password)
    {
        UserId = userId;
        Password = password;
    }

    public Guid UserId { get; set; }
    public string Password { get; set; }
}
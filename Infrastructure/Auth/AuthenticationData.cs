namespace Infrastructure.Auth;

public class AuthenticationData
{
    public AuthenticationData(Guid contactId, string password)
    {
        ContactId = contactId;
        Password = password;
    }

    public Guid ContactId { get; set; }
    public string Password { get; set; }
}
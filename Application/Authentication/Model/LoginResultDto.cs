namespace Application.Authentication.Model;

public class LoginResultDto
{
    public LoginResultDto(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
    }

    public Guid UserId { get; set; }
    public string Token { get; set; }
}
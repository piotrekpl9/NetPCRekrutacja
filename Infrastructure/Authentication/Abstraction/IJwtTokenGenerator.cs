namespace Infrastructure.Authentication.Abstraction;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string email);
}
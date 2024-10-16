using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Authentication.Abstraction;
using Infrastructure.Authentication.Model;
using Infrastructure.Common;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(Guid userId, string email)
    {
        var secret = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var signingCredentials = new SigningCredentials( 
            new SymmetricSecurityKey(secret),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email,email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, 
            audience: _jwtSettings.Audience, 
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes), 
            claims: claims, 
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
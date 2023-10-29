using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoManager.Application.Interfaces.Authentication;
using TodoManager.Application.Models.Users;

namespace TodoManager.Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        var expiresInMinutes = _jwtSettings.ExpiryMinutes;
        var expirationDate = DateTime.UtcNow.AddMinutes(expiresInMinutes);
        var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                DateTime.UtcNow,
                expirationDate,
                signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

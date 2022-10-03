using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoManager.Core.Helpers;
using TodoManager.Core.Models;
using TodoManager.Core.Services;

namespace TodoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IConfiguration _config;

    public AuthController(IUsersService users, IConfiguration config)
    {
        _usersService = users;
        _config = config;
    }

    public record AuthenticateRequest(string? UserName, string? Password);
    
    
    [HttpPost("token")]
    public ActionResult<string> Authenticate(AuthenticateRequest credentials)
    {
        var user = ValidateCredentials(credentials);

        if (user is null)
            return Unauthorized();

        string token = GenerateToken(user);

        return Ok(user);
    }

    private User? ValidateCredentials(AuthenticateRequest credentials)
    {
        if (credentials.UserName is null || credentials.UserName.Length <= 0)
            return null;

        var user = _usersService.GetUserWithPassword(credentials.UserName);
        bool isVerified = SecretHasher.Verify(credentials.Password, user.Password);

        if(isVerified)
            return user;

        return null;
    }

    private string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            _config.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer: _config.GetValue<string>("Authentication:Issuer"),
                audience: _config.GetValue<string>("Authentication:Audience"),
                claims: new List<Claim> { new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()) },
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

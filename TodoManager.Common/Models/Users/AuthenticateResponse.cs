using TodoManager.Common.Entities;

namespace TodoManager.Common.Models.Users;

public class AuthenticateResponse
{
    public User User{ get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(User user, string token)
    {
        User = user;
        Token = token;
    }
}

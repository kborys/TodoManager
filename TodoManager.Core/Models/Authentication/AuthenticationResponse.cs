using TodoManager.Application.Models.Users;

namespace TodoManager.Application.Models.Authentication;

public class AuthenticationResponse
{
    public User User { get; set; }
    public string Token { get; set; }

    public AuthenticationResponse(User user, string token)
    {
        User = user;
        Token = token;
    }
}

using System.ComponentModel.DataAnnotations;

namespace TodoManager.Application.Models.Authentication;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    public LoginRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}

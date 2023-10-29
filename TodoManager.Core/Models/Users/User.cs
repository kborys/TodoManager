using System.Text.Json.Serialization;

namespace TodoManager.Application.Models.Users;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;

    private User() //dapper purpose
    {

    }

    public User(string userName, string firstName, string lastName, string passwordHash, string emailAddress, int userId = 0)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        EmailAddress = emailAddress;
    }
}

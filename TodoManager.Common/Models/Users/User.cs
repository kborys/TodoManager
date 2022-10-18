using System.Text.Json.Serialization;

namespace TodoManager.Common.Models.Users;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
    public string EmailAddress { get; set; }

    private User() //dapper purpose
    {

    }

    public User(string userName, string firstName, string lastName, string password, string emailAddress, int userId = 0)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        EmailAddress = emailAddress;
    }
}

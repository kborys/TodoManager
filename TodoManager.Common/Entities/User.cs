using System.Text.Json.Serialization;

namespace TodoManager.Common.Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}

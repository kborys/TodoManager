using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Users;

public class UpdateRequest
{
    [MinLength(2)]
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MinLength(2)]
    [MaxLength(50)]
    public string? LastName { get; set; }

    [MinLength(8)]
    public string? Password { get; set; }

    public UpdateRequest(string? firstName = null, string? lastName = null, string? password = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}

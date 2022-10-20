using TodoManager.Common.Attributes;

namespace TodoManager.Common.Models.Users;

public class UserUpdateRequest
{
    [EmptyOrStringLength(Minimum = 2, Maximum = 50, ErrorMessage = "Must be either empty or contain between 2 and 50 characters")]
    public string? FirstName { get; set; }

    [EmptyOrStringLength(Minimum = 2, Maximum = 50, ErrorMessage = "Must be either empty or contain between 2 and 50 characters")]
    public string? LastName { get; set; }

    [EmptyOrStringLength(Minimum = 7, ErrorMessage = "Must be either empty or contain more than 7 characters")]
    public string? Password { get; set; }

    public UserUpdateRequest(string? firstName = null, string? lastName = null, string? password = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}

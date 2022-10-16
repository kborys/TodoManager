using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Users;

public class CreateRequest
{
    [Required]
    [MinLength(6)]
    [MaxLength(32)]
    public string UserName { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }

    public CreateRequest(string userName, string firstName, string lastName, string password, string emailAddress)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        EmailAddress = emailAddress;
    }
}

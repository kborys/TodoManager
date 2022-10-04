using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Users;

public class CreateRequest
{
    [Required]
    [MinLength(6)]
    [MaxLength(32)]
    //TODO: add regex for https://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap03.html#tag_03_282
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
}

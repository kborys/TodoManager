using System.ComponentModel.DataAnnotations;

namespace TodoManager.Application.Models.Groups;

public class AddGroupMemberRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;
}

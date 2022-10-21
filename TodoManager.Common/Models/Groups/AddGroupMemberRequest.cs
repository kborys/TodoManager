using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Groups;

public class AddGroupMemberRequest
{
    [Required]
    public string UserName{ get; set; } = string.Empty;
}

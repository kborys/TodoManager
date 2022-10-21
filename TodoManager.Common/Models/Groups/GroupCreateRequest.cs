using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Groups;

public class GroupCreateRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Name { get; set; }

    public GroupCreateRequest(string name)
    {
        Name = name;
    }
}

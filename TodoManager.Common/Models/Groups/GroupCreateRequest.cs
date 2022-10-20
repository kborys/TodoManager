using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Groups;

public class GroupCreateRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue)]
    public int OwnerId { get; set; }

    public GroupCreateRequest(string name, int ownerId)
    {
        Name = name;
        OwnerId = ownerId;
    }
}

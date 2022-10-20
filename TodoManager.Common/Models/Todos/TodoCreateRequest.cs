using System.ComponentModel.DataAnnotations;
using TodoManager.Common.Attributes;
using TodoManager.Common.Models.Enums;

namespace TodoManager.Common.Models.Todos;

public class TodoCreateRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    [EmptyOrStringLength(Minimum = 2, Maximum = 250, ErrorMessage = "Must be either empty or contain between 2 and 250 characters")]
    public string? Description { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int GroupId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int OwnerId { get; set; }

    [Required]
    [Range(1, 5)]
    public Status Status { get; set; }
}

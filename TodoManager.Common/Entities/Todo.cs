using TodoManager.Common.Models.Enums;

namespace TodoManager.Common.Entities;

public class Todo
{
    public int TodoId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Group Group{ get; set; }
    public User Owner { get; set; }
    public Status Status { get; set; }
}

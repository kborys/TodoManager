using TodoManager.Common.Entities;
using TodoManager.Common.Models.Enums;
using TodoManager.Common.Models.Groups;

namespace TodoManager.Common.Models.Todos;

public class Todo
{
    public int TodoId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int GroupId { get; set; }
    public int OwnerId { get; set; }
    public Status Status { get; set; }
}

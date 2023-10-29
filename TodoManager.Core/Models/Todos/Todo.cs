using TodoManager.Application.Models.Enums;

namespace TodoManager.Application.Models.Todos;

public class Todo
{
    public int TodoId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int GroupId { get; set; }
    public int OwnerId { get; set; }
    public Status Status { get; set; }

    private Todo() //dapper purpose
    {
    }

    public Todo(int todoId, string title, int groupId, int ownerId, Status status, string? description = null)
    {
        TodoId = todoId;
        Title = title;
        GroupId = groupId;
        OwnerId = ownerId;
        Status = status;

        if (description is not null)
            Description = description;
    }
}

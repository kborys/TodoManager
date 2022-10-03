namespace TodoManager.Core.Models;

public class TodoModel
{
    public int TodoId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FinishUntil { get; set; }
    public bool IsComplete { get; set; }
    public int UserId { get; set; }
}

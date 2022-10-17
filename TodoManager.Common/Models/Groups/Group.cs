using TodoManager.Common.Entities;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Common.Models.Groups;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
}

namespace TodoManager.Application.Models.Groups;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OwnerId { get; set; }

    private Group() //dapper purpose
    {
    }

    public Group(string name, int ownerId = 0, int groupId = 0)
    {
        GroupId = groupId;
        Name = name;
        OwnerId = ownerId;
    }
}

namespace TodoManager.Common.Entities;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public User Owner { get; set; }
    public List<Todo> Todos { get; set; }
    public List<User> Members { get; set; }
}

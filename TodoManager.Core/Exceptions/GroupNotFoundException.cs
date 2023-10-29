namespace TodoManager.Application.Exceptions;

public class GroupNotFoundException : NotFoundException
{
    public GroupNotFoundException() : base("Group with given id does not exist.")
    {
    }
}

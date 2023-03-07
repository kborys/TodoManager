namespace TodoManager.Common.Exceptions;

public class NotMemberException : UnauthorizedAccessException
{
    public NotMemberException() : base("You must be a group member in order to do this.") { }
}

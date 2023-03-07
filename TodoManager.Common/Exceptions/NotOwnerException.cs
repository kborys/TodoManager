namespace TodoManager.Common.Exceptions;

public class NotOwnerException : UnauthorizedAccessException
{
    public NotOwnerException() : base("You must be the group owner in order to do this.") { }
}
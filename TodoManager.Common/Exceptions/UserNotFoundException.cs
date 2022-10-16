namespace TodoManager.Common.Exceptions;

public class UserNotFoundException : NotFoundException
{
	public UserNotFoundException(int id) : base($"User with Id: {id} doesn't exist in the database")
	{
	}
}

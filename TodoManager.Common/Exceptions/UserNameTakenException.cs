namespace TodoManager.Common.Exceptions;

public  class UserNameTakenException : Exception
{
	public UserNameTakenException(string userName) : base($"Username '{userName}' is already taken. Please try again.")
	{
	}
}

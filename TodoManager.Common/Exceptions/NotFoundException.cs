namespace TodoManager.Common.Exceptions;

public abstract class NotFoundException : Exception
{
	public NotFoundException(string message) : base(message)
	{
	}
}

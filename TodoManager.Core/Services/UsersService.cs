using TodoManager.Core.Helpers;
using TodoManager.Core.Models;

namespace TodoManager.Core.Services;

public class UsersService : IUsersService
{
	public UsersService()
	{

	}

	public User GetUserWithPassword(string userName)
	{
		return new User { UserId = 1, FirstName = "Konrad", LastName = "Boryś", Password = SecretHasher.Hash("testPassword"), UserName = userName };
	}
}

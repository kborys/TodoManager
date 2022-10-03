using TodoManager.Core.Models;

namespace TodoManager.Core.Services
{
    public interface IUsersService
    {
        User GetUserWithPassword(string userName);
    }
}
using TodoManager.Application.Models.Authentication;
using TodoManager.Application.Models.Users;

namespace TodoManager.Application.Interfaces.Repositories;
public interface IUserRepository
{
    Task<int> Create(RegisterRequest request);
    Task<User?> GetById(int id);
    Task<User?> GetByUserName(string userName);
    Task Update(User user);
    Task Delete(int id);
    Task<int> Count(string userName, string emailAddress);
}

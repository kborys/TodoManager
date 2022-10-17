using TodoManager.Common.Entities;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts;
public interface IUserRepository
{
    Task<int> Create(CreateRequest request);
    Task<User?> GetById(int id);
    Task<User?> GetByUserName(string userName);
    Task Update(User user);
    Task Delete(int id);
    Task<int> Count(string userName, string emailAddress);
}

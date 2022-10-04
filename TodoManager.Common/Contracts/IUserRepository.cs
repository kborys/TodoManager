using TodoManager.Common.Entities;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts;
public interface IUserRepository
{
    void Create(CreateRequest model);
    User? GetById(int id);
    User? GetByUserName(string userName);
    void Update(User user);
    void Delete(int id);
}

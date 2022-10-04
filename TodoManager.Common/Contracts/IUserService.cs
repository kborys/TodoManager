using TodoManager.Common.Entities;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts;

public interface IUserService
{
    AuthenticateResponse? Authenticate(AuthenticateRequest model);
    void Create(CreateRequest model);
    User? GetById(int id);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
}
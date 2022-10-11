using TodoManager.Common.Entities;
using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
    Task Create(CreateRequest model);
    Task<User?> GetById(int id);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}
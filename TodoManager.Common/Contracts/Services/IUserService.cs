using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Services;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
    Task<User> Create(CreateRequest request);
    Task<User?> GetById(int id);
    Task Update(int id, UpdateRequest request);
    Task Delete(int id);
}
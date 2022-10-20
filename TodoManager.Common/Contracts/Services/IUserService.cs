using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Services;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
    Task<User> Create(UserCreateRequest request);
    Task<User?> GetById(int id);
    int GetActiveUserId();
    Task Update(int id, UserUpdateRequest request);
    Task Delete(int id);
}
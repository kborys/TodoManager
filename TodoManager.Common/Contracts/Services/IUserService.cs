using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts.Services;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
    Task<User> Create(UserCreateRequest request);
    Task<User?> GetById(int userId);
    Task<User?> GetByUserName(string userName);
    Task Update(int userId, UserUpdateRequest request, int activeUserId);
    Task Delete(int userId, int activeUserId);
}
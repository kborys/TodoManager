using TodoManager.Application.Models.Authentication;
using TodoManager.Application.Models.Users;

namespace TodoManager.Application.Interfaces.Services;

public interface IUserService
{
    Task<AuthenticationResponse?> Authenticate(LoginRequest request);
    Task<User> Create(RegisterRequest request);
    Task<User?> GetById(int userId);
    Task<User?> GetByUserName(string userName);
    Task Update(int userId, UserUpdateRequest request, int activeUserId);
    Task Delete(int userId, int activeUserId);
}
using TodoManager.Application.Models.Users;

namespace TodoManager.Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}
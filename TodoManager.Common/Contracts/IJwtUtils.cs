using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts;

public interface IJwtUtils
{
    string GenerateToken(User user);
}
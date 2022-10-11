using TodoManager.Common.Entities;

namespace TodoManager.Common.Contracts;

public interface IJwtUtils
{
    string GenerateToken(User user);
}
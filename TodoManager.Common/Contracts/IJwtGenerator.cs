using TodoManager.Common.Models.Users;

namespace TodoManager.Common.Contracts;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}
namespace TodoManager.Application.Interfaces.Authentication;

public interface IPasswordHasher
{
    string Hash(string secret);
    bool Verify(string password, string hash);
}

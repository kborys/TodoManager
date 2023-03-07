using System.Security.Claims;

namespace TodoManager.Api.Helpers;

public interface IAuthHelper
{
    int GetActiveUserId();
}

public class AuthHelper : IAuthHelper
{
    private readonly IHttpContextAccessor _accessor;

    public AuthHelper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public int GetActiveUserId()
    {
        if (_accessor is null || _accessor.HttpContext is null) return 0;
        string userIdText = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        return int.Parse(userIdText);
    }
}

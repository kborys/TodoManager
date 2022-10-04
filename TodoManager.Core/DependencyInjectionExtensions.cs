using Microsoft.Extensions.DependencyInjection;
using TodoManager.Common.Contracts;
using TodoManager.Core.Services;

namespace TodoManager.Core;

public static class DependencyInjectionExtensions
{
    public static void AddCoreModules(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}

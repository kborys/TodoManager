using Microsoft.Extensions.DependencyInjection;
using TodoManager.Common.Contracts;
using TodoManager.Common.Contracts.Services;
using TodoManager.Core.Helpers;
using TodoManager.Core.Services;

namespace TodoManager.Core;

public static class DependencyInjectionExtensions
{
    public static void AddCoreModules(this IServiceCollection services)
    {
        services.AddTransient<IJwtUtils, JwtUtils>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IGroupService, GroupService>();
    }
}

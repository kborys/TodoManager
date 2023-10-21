using Microsoft.Extensions.DependencyInjection;
using TodoManager.Common.Contracts;
using TodoManager.Common.Contracts.Services;
using TodoManager.Core.Helpers;
using TodoManager.Core.Services;

namespace TodoManager.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        return services
            .AddTransient<IJwtGenerator, JwtGenerator>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITodoService, TodoService>()
            .AddScoped<IGroupService, GroupService>();
    }
}

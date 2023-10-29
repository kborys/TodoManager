using Microsoft.Extensions.DependencyInjection;
using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Services;

namespace TodoManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITodoService, TodoService>()
            .AddScoped<IGroupService, GroupService>();
    }
}

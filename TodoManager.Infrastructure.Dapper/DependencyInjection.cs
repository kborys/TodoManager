using Microsoft.Extensions.DependencyInjection;
using TodoManager.Application.Interfaces.Repositories;
using TodoManager.Infrastructure.Dapper.Repository;

namespace TodoManager.Infrastructure.Dapper;

public static class DependencyInjection
{
    public static void AddDapper(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
    }
}

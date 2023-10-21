using Microsoft.Extensions.DependencyInjection;
using TodoManager.Common.Contracts.Repositories;
using TodoManager.Infrastructure.Dapper.Repository;

namespace TodoManager.Infrastructure.Dapper;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
    }
}

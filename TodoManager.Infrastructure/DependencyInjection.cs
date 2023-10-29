using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TodoManager.Infrastructure.Authentication;
using TodoManager.Infrastructure.Dapper;
using TodoManager.Application.Interfaces.Authentication;

namespace TodoManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDapper();
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services
            .AddSingleton<IJwtGenerator, JwtGenerator>()
            .AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
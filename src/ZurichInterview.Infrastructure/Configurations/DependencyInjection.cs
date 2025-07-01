using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZurichInterview.Application.Interfaces.Authentication;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Infrastructure.Authentication;
using ZurichInterview.Infrastructure.Persistence;
using ZurichInterview.Infrastructure.Services;

namespace ZurichInterview.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string? sqlStringConnection)
    {
        //DBContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(sqlStringConnection));
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IPolicyService, PolicyService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}
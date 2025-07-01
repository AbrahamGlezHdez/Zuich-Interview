using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ZurichInterview.Application.Mapping;
using ZurichInterview.Application.Validators.Client;
using ZurichInterview.Application.Validators.Policy;

namespace ZurichInterview.Application.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<PolicyDtoValidator>();

        services.AddAutoMapper(typeof(ClientProfile).Assembly);
        return services;
    }
}
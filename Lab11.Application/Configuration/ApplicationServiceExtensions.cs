using Lab.Application;
using Lab.Application.Common.Interfaces.Persistence;
using Lab.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab11.Application.Configuration;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        // 
        
        services.AddScoped<NotificationService>();
        services.AddScoped<CleanupService>();
        return services;
    }
}
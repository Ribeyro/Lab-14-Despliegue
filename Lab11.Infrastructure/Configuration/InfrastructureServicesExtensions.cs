using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Interfaces.IServices;
using Lab11.Infrastructure.Implements;
using Lab11.Infrastructure.Implements.Services;
using Lab11.Infrastructure.Implements.UnitOfWork;
using Lab11.Infrastructure.Persistence.Data;
using Lab11.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab11.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // ✅ Registro del DbContext (usa Pomelo para MySQL)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // ✅ Repositorios, UnitOfWork 
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<ExcelReportService>();

        return services;
    }
}
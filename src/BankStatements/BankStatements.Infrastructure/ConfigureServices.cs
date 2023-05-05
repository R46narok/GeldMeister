using BankStatements.Application.Common.Repositories;
using BankStatements.Infrastructure.Persistence;
using BankStatements.Infrastructure.Repositories;
using BankStatements.Infrastructure.Services;
using GeldMeister.Common.Application.Interfaces;
using GeldMeister.Common.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankStatements.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IDateTime, DateTimeService>();

        services.AddDbContext<BankStatementsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                builder => builder.MigrationsAssembly(typeof(BankStatementsDbContext).Assembly.FullName)));

        services.AddScoped<BankStatementsDbContextInitialiser>();

        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IBankSchemeRepository, BankSchemeRepository>();
        services.AddScoped<IBankSchemePropertyRepository, BankSchemePropertyRepository>();

        return services;
    }
}
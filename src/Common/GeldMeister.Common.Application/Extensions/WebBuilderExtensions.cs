using System.Reflection;
using FluentValidation;
using GeldMeister.Common.Application.Behaviours;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeldMeister.Common.Application.Extensions;

public static class WebBuilderExtensions
{
    public static IServiceCollection AddMediatorAndFluentValidation(this IServiceCollection services,
        Assembly[] assemblies)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddAutoMapper(assemblies);

        return services;
    }

    public static void AddPersistence<T>(this WebApplicationBuilder builder, string name = "Database")
        where T : DbContext
    {
        var connectionString = builder.Configuration.GetConnectionString(name);
        builder.Services.AddDbContext<T>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        );
    }

    public static void UsePersistence<T>(this WebApplication app) where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<T>();

        db?.Database.EnsureCreated();
    }
}
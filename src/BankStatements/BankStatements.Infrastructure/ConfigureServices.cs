﻿using BankStatements.Application.Common.Builders;
using BankStatements.Application.Common.Dto;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Application.Common.Repositories;
using BankStatements.Infrastructure.Files;
using BankStatements.Infrastructure.Memory;
using BankStatements.Infrastructure.Persistence;
using BankStatements.Infrastructure.Persistence.Builders;
using BankStatements.Infrastructure.Repositories;
using BankStatements.Infrastructure.Services;
using GeldMeister.Common.Application.Interfaces;
using GeldMeister.Common.Infrastructure.Extensions;
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
        services.AddEventHandlers(typeof(BankDto).Assembly);

        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IBankSchemeRepository, BankSchemeRepository>();
        services.AddScoped<IBankSchemePropertyRepository, BankSchemePropertyRepository>();
        services.AddScoped<IBankStatementRepository, BankStatementRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionFieldRepository, TransactionFieldRepository>();

        services.AddSingleton<IBinarySerializer, BinarySerializer>();
        
        services.AddTransient<ITransactionQueryBuilder, MssqlTransactionQueryBuilder>();
        
        services.AddSingleton<IBankStatementParserFactory, BankStatementParserFactory>();
        services.AddSingleton<ISqlConnectionFactory, DapperSqlConnectionFactory>();
        

        return services;
    }
}
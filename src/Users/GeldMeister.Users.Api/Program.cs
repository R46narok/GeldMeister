using GeldMeister.Common.Application.Extensions;
using GeldMeister.Common.Application.MessageBrokers;
using GeldMeister.Common.Application.Security;
using GeldMeister.Common.Infrastructure.MessageBrokers;
using GeldMeister.Users.Data.Entities;
using GeldMeister.Users.Data.Persistence;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;
using Serilog;

var factory = new ConnectionFactory() {HostName = "localhost"};

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
// Data layer

builder.AddPersistence<UserDbContext>();

// Mediator 
builder.Services.AddMediatorAndFluentValidation(new[] {typeof(User).Assembly});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();
builder.AddJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePersistence<UserDbContext>();
app.UseJwtAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
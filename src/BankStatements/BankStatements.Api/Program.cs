using BankStatements.Api.Security;
using BankStatements.Api.Services;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Application.Common.Repositories;
using BankStatements.Infrastructure;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Extensions;
using GeldMeister.Common.Application.MessageBrokers;
using GeldMeister.Common.Application.Security;
using GeldMeister.Common.Infrastructure.Extensions;
using GeldMeister.Common.Infrastructure.MessageBrokers;
using RabbitMQ.Client;

var factory = new ConnectionFactory() {HostName = "localhost"};
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(builder.Configuration);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddMediatorAndFluentValidation(new[] {typeof(IBankRepository).Assembly});
builder.AddJwtAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseJwtAuthentication();
app.UseAuthorization();
app.UseEventHandlers();

app.MapControllers();

app.Run();
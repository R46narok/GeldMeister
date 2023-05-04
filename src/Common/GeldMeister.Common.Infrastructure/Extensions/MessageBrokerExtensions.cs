using System.Reflection;
using GeldMeister.Common.Infrastructure.MessageBrokers.RabbitMQ;
using GeldMeister.Common.Integration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace GeldMeister.Common.Infrastructure.Extensions;

public static class MessageBrokerExtensions
{
    private static List<Type> _registered = new();

    public static void UseEventHandlers(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        foreach (var type in _registered)
        {
            var generic = typeof(RabbitReceiver<>).MakeGenericType(type);
            scope.ServiceProvider.GetService(generic);
        }
    }
    
    public static void AddEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlers = assembly
            .GetTypes()
            .Where(x => x.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)))
            .ToList();

        var method = typeof(MessageBrokerExtensions)
            .GetMethod(nameof(AddRabbitReceiver), BindingFlags.Static | BindingFlags.NonPublic)!;

        foreach (var handler in handlers)
        {
            var @interface = handler
                .GetInterfaces()
                .Single(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>));
            var @event = @interface.GetGenericArguments().Single();
            
            _registered.Add(@event);
            
            services.AddScoped(@interface, handler);
            
            var genericMethod = method.MakeGenericMethod(@event);
            genericMethod.Invoke(null, new object?[]{ services, @event.Name});
        } 
    }

    private static void AddRabbitReceiver<T>(IServiceCollection collection, string exchange) where T : IIntegrationEvent
    {
        collection.AddSingleton(provider =>
        {
            var connection = provider.GetService<IConnection>()!;
            return new RabbitReceiver<T>(connection, provider, exchange);
        });
    }
}
using GeldMeister.Common.Application.MessageBrokers;
using GeldMeister.Common.Infrastructure.MessageBrokers.RabbitMQ;
using GeldMeister.Common.Integration;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace GeldMeister.Common.Infrastructure.MessageBrokers;

public class MessagePublisher : IMessagePublisher
{
    private readonly List<IMessageSenderFactory> _factories;

    public MessagePublisher(IConfiguration configuration, IServiceProvider provider)
    {
        _factories = new();

        try
        {
            var providers = configuration
                .GetSection("Messaging:Providers")
                .Get<string[]>();
            if (providers is null) return;

            if (providers.Contains("RabbitMQ"))
            {
                var connection = (IConnection) provider.GetService(typeof(IConnection))!;
                _factories.Add(new RabbitSenderFactory(connection));
            }
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    public async Task PublishTopicAsync<T>(T message, MessageMetadata metadata,
        CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
        foreach (var factory in _factories)
        {
            var sender = factory.CreateTopicSender<T>();
            await sender.SendAsync(message, metadata, cancellationToken);
        }
    }

    public async Task PublishQueueAsync<T>(T message, MessageMetadata metadata,
        CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
        foreach (var factory in _factories)
        {
            var sender = factory.CreateQueueSender<T>();
            await sender.SendAsync(message, metadata, cancellationToken);
        }
    }
}
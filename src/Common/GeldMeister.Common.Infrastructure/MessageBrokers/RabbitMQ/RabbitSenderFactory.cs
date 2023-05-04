using GeldMeister.Common.Application.MessageBrokers;
using GeldMeister.Common.Integration;
using RabbitMQ.Client;

namespace GeldMeister.Common.Infrastructure.MessageBrokers.RabbitMQ;

public class RabbitSenderFactory : IMessageSenderFactory
{
    private readonly IConnection _connection;

    public RabbitSenderFactory(IConnection connection)
    {
        _connection = connection;
    }
    
    public IMessageSender<T> CreateTopicSender<T>() where T : IIntegrationEvent
    {
        return new RabbitTopicSender<T>(_connection);
    }

    public IMessageSender<T> CreateQueueSender<T>() where T : IIntegrationEvent
    {
        return new RabbitQueueSender<T>(_connection);
    }
}
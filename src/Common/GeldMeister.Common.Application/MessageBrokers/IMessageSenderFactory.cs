using GeldMeister.Common.Integration;

namespace GeldMeister.Common.Application.MessageBrokers;

public interface IMessageSenderFactory
{
    public IMessageSender<T> CreateTopicSender<T>() where T : IIntegrationEvent;
    public IMessageSender<T> CreateQueueSender<T>() where T : IIntegrationEvent;
}
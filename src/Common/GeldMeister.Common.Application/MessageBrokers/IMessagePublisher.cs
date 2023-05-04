
using GeldMeister.Common.Integration;

namespace GeldMeister.Common.Application.MessageBrokers;

public interface IMessagePublisher
{
   public Task PublishTopicAsync<T>(T message, MessageMetadata metadata, CancellationToken cancellationToken = default) where T : IIntegrationEvent;
   public Task PublishQueueAsync<T>(T message, MessageMetadata metadata, CancellationToken cancellationToken = default) where T : IIntegrationEvent;
}
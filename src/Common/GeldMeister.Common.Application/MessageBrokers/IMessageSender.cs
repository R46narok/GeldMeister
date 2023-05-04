
using GeldMeister.Common.Integration;

namespace GeldMeister.Common.Application.MessageBrokers;

public interface IMessageSender<in T> where T : IIntegrationEvent
{
   public Task SendAsync(T message, MessageMetadata metadata, CancellationToken cancellationToken = default);
}

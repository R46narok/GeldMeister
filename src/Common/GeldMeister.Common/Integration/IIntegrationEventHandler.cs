namespace GeldMeister.Common.Integration;

public interface IIntegrationEventHandler<T> where T : IIntegrationEvent
{
    Task HandleAsync(T domainEvent, CancellationToken cancellationToken = default);
}
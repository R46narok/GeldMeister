using GeldMeister.Common.Integration;

namespace BankStatements.Application.Users.EventHandlers.Create;

public class UserCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public string CorrelationId { get; init; }
}
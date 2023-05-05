using GeldMeister.Common.Integration;

namespace BankStatements.Application.Users.EventHandlers.Create;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    public async Task HandleAsync(UserCreatedIntegrationEvent domainEvent, CancellationToken cancellationToken = default)
    {
        
    }
}
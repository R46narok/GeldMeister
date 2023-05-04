namespace BankStatements.Domain.UserAggregate.Events;

public record UserCreatedDomainEvent(Guid Id, Guid CorrelationId) : DomainEvent(Id);
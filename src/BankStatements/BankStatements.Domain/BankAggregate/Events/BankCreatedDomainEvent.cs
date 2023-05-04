namespace BankStatements.Domain.BankAggregate.Events;

public record BankCreatedDomainEvent(Guid Id, Guid CorrelationId) : DomainEvent(Id);

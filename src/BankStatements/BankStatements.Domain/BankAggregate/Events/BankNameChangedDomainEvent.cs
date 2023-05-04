namespace BankStatements.Domain.BankAggregate.Events;

public record BankNameChangedDomainEvent(Guid Id, Guid CorrelationId) : DomainEvent(Id);
namespace BankStatements.Domain.BankAggregate.Events;

public record TransactionCreatedDomainEvent(Guid Id, Guid CorrelationId) : DomainEvent(Id);
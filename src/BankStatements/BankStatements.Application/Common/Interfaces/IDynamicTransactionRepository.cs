using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Interfaces;

public interface IDynamicTransactionRepository
{
    public Task<bool> IsTransactionTypeCreated(string name);
    public Task CreateTransactionType(string name, BankScheme scheme);

    public Task CreateTransaction(string name, BankScheme scheme, Guid statementId, Dictionary<string, object> parameters);
}
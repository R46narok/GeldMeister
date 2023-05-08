using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Interfaces;

public interface IDynamicTransactionRepository
{
    public Task<bool> IsTransactionTypeCreated(string name);
    public Task CreateTransactionType(string name, BankScheme scheme);
    public Task RenameTransactionType(string oldName, string newName);

    public Task CreateTransaction(string name, BankScheme scheme, Guid statementId, IDictionary<string, object> parameters);


    public Task<List<dynamic>> GetTransactionsByStatementId(string name, Guid statementId);
    public Task<List<dynamic>> GetTransactionsByStatementIdWithPagination(
        string name, Guid statementId, int pageIndex, int pageSize);
}
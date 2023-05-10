using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Infrastructure.Repositories;

public class TransactionFieldRepository 
    : RepositoryBase<TransactionField, Guid, BankStatementsDbContext>, ITransactionFieldRepository
{
    public TransactionFieldRepository(BankStatementsDbContext context) : base(context)
    {
    }
}
